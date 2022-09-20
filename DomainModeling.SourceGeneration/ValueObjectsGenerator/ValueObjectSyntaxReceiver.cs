using CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator;

internal static class ValueObjectSyntaxReceiver
{
	private const string AttributeNamespace			= "CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects";
	private const string DefaultAttributeName		= "GenerateValueObject";
	private const string StringAttributeName		= "GenerateStringValueObject";
	private const string ListAttributeName			= "GenerateListValueObjectAttribute";
	private const string DictionaryAttributeName	= "GenerateDictionaryValueObject";
	
	public static bool CheckIfProbablyIsValueObjectToGenerate(SyntaxNode node, CancellationToken cancellationToken)
	{
		if (node is not RecordDeclarationSyntax and not StructDeclarationSyntax and not ClassDeclarationSyntax)
			return false;
		
		var attribute = ((TypeDeclarationSyntax)node).AttributeLists
			.SelectMany(list => list.Attributes)
			.SingleOrDefault(attribute =>
				attribute.Name.HasAttributeName(DefaultAttributeName, cancellationToken)
				|| attribute.Name.HasAttributeName(StringAttributeName, cancellationToken)
				|| attribute.Name.HasAttributeName(ListAttributeName, cancellationToken)
				|| attribute.Name.HasAttributeName(DictionaryAttributeName, cancellationToken));

		return attribute is not null;
	}

	public static ValueObjectBase? GetModel(TypeDeclarationSyntax typeDeclarationSyntax, SemanticModel semanticModel)
	{
		var type = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax);

		if (type is null || type.IsStatic || !typeDeclarationSyntax.Modifiers.Any(m =>  m.IsKind(SyntaxKind.PartialKeyword))) 
			return null;
		
		if (type.TypeKind != TypeKind.Struct && type.TypeKind != TypeKind.Class) return null;

		var hasDefaultAttribute = type.HasAttribute(DefaultAttributeName, AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 1);
		var hasStringAttribute = attribute is null && type.HasAttribute(StringAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0);
		var hasListAttribute = attribute is null && type.HasAttribute(ListAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 1);
		var hasDictionaryAttribute = attribute is null && type.HasAttribute(DictionaryAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 2);
		
		if (attribute is null) return null;

		var declaration = GetDeclaration(typeDeclarationSyntax, type.GetTypeNameWithGenericParameters());
		var generateToString = attribute.GetArgumentOrDefault("generateToString", true);
		var generateComparison = attribute.GetArgumentOrDefault("generateComparison", true);
		var addCustomValidation = attribute.GetArgumentOrDefault("addCustomValidation", true);
		var generateDefaultConstructor = attribute.GetArgumentOrDefault("generateDefaultConstructor", true);
		var generateParameterlessConstructor = attribute.GetArgumentOrDefault("generateParameterlessConstructor", false);
		var generateEmptyStatic = attribute.GetArgumentOrDefault("generateEmptyStatic", false);
		var propertyName = attribute.GetArgumentOrDefault("propertyName", (string?)null);

		if (hasDefaultAttribute)
			return new DefaultValueObject(
				ValueObjectType: type,
				Attribute: attribute,
				Declaration: declaration,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				GenerateDefaultConstructor: generateDefaultConstructor,
				GenerateParameterlessConstructor: generateParameterlessConstructor,
				GenerateEmptyStatic: generateEmptyStatic,
				PropertyName: propertyName,
				AllowNull: attribute.GetArgumentOrDefault("allowNull", false),
				MinimumValue: attribute.TryGetArgument<int>("minimumValue", out var minimumValue) && minimumValue != Int32.MinValue ? minimumValue : null,
				MaximumValue: attribute.TryGetArgument<int>("maximumValue", out var maximumValue) && maximumValue != Int32.MinValue ? maximumValue : null);

		if (hasStringAttribute)
			return new StringValueObject(
				ValueObjectType: type,
				Declaration: declaration,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				GenerateDefaultConstructor: generateDefaultConstructor,
				GenerateParameterlessConstructor: generateParameterlessConstructor,
				GenerateEmptyStatic: generateEmptyStatic,
				PropertyName: propertyName,
				AllowNull: attribute.GetArgumentOrDefault("allowNull", false),
				MinimumLength: attribute.TryGetArgument<int>("minimumLength", out var minimumLength) && minimumLength != Int32.MinValue ? minimumLength : null,
				MaximumLength: attribute.TryGetArgument<int>("maximumLength", out var maximumLength) && maximumLength != Int32.MinValue ? maximumLength : null,
				StringCaseConversion: attribute.GetArgumentOrDefault("stringCaseConversion", StringCaseConversion.NoConversion),
				StringFormat: attribute.GetArgumentOrDefault("stringFormat", StringFormat.Default),
				CompareOptions: attribute.GetArgumentOrDefault("compareOptions", StringComparison.Ordinal));

		if (hasListAttribute)
			return new ListValueObject(
				ValueObjectType: type,
				Attribute: attribute,
				Declaration: declaration,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				GenerateDefaultConstructor: generateDefaultConstructor,
				GenerateParameterlessConstructor: generateParameterlessConstructor,
				GenerateEmptyStatic: generateEmptyStatic,
				PropertyName: propertyName,
				MinimumCount: attribute.TryGetArgument<int>("minimumCount", out var minimumCount) && minimumCount != Int32.MinValue ? minimumCount : null,
				MaximumCount: attribute.TryGetArgument<int>("maximumCount", out var maximumCount) && maximumCount != Int32.MinValue ? maximumCount : null);
		
		if (hasDictionaryAttribute)
			return new DictionaryValueObject(
				ValueObjectType: type,
				Attribute: attribute,
				Declaration: declaration,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				GenerateDefaultConstructor: generateDefaultConstructor,
				GenerateParameterlessConstructor: generateParameterlessConstructor,
				GenerateEmptyStatic: generateEmptyStatic,
				PropertyName: propertyName,
				MinimumCount: attribute.TryGetArgument<int>("minimumCount", out var minimumCount) && minimumCount != Int32.MinValue ? minimumCount : null,
				MaximumCount: attribute.TryGetArgument<int>("maximumCount", out var maximumCount) && maximumCount != Int32.MinValue ? maximumCount : null);
		return null;

		static string GetDeclaration(TypeDeclarationSyntax declaration, string name)
		{
			var declarationText = declaration.ToFullString();

			var start = declarationText.IndexOf(']') + 1;
			var end = declarationText.IndexOf('{');
			if (end == -1) end = declarationText.IndexOf(';');
			
			declarationText = end == -1
				? declarationText.Substring(start)
				: declarationText.Substring(start, end - start).Trim();
			
			return declarationText;
		}
	}
}