using CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator;

internal static class ValueObjectSyntaxReceiver
{
	private const string AttributeNamespace			= "CodeChops.DomainDrivenDesign.DomainModeling.Attributes";
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

		var declaration = GetDeclaration(typeDeclarationSyntax, type.Name);
		var generateToString = attribute.GetArgumentOrDefault("generateToString", true);
		var addCustomValidation = attribute.GetArgumentOrDefault("addCustomValidation", false);
		var prohibitParameterlessConstruction = attribute.GetArgumentOrDefault("prohibitParameterlessConstruction", true);
		var generateEmptyStatic = attribute.GetArgumentOrDefault("generateEmptyStatic", true);

		if (hasDefaultAttribute)
			return new DefaultValueObject(
				Type: type,
				Attribute: attribute,
				Declaration: declaration,
				GenerateToString: generateToString,
				AddCustomValidation: addCustomValidation,
				ProhibitParameterlessConstruction: prohibitParameterlessConstruction,
				GenerateEmptyStatic: generateEmptyStatic,
				MinimumValue: attribute.TryGetArgument<int>("minimumValue", out var minimumValue) && minimumValue != Int32.MinValue ? minimumValue : null,
				MaximumValue: attribute.TryGetArgument<int>("maximumValue", out var maximumValue) && maximumValue != Int32.MinValue ? maximumValue : null);

		if (hasStringAttribute)
			return new StringValueObject(
				Type: type,
				Attribute: attribute,
				Declaration: declaration,
				GenerateToString: generateToString,
				AddCustomValidation: addCustomValidation,
				ProhibitParameterlessConstruction: prohibitParameterlessConstruction,
				GenerateEmptyStatic: generateEmptyStatic,
				MinimumLength: attribute.TryGetArgument<int>("minimumLength", out var minimumLength) && minimumLength != Int32.MinValue ? minimumLength : null,
				MaximumLength: attribute.TryGetArgument<int>("maximumLength", out var maximumLength) && maximumLength != Int32.MinValue ? maximumLength : null,
				StringCaseConversion: attribute.GetArgumentOrDefault("stringCaseConversion", StringCaseConversion.NoConversion),
				StringFormat: attribute.GetArgumentOrDefault("stringFormat", StringFormat.Default),
				CompareOptions: attribute.GetArgumentOrDefault("compareOptions", StringComparison.Ordinal));

		if (hasListAttribute)
			return new ListValueObject(
				Type: type,
				Attribute: attribute,
				Declaration: declaration,
				GenerateToString: generateToString,
				AddCustomValidation: addCustomValidation,
				ProhibitParameterlessConstruction: prohibitParameterlessConstruction,
				GenerateEmptyStatic: generateEmptyStatic,
				MinimumCount: attribute.TryGetArgument<int>("minimumCount", out var minimumCount) && minimumCount != Int32.MinValue ? minimumCount : null,
				MaximumCount: attribute.TryGetArgument<int>("maximumCount", out var maximumCount) && maximumCount != Int32.MinValue ? maximumCount : null);
		
		if (hasDictionaryAttribute)
			return new DictionaryValueObject(
				Type: type,
				Attribute: attribute,
				Declaration: declaration,
				GenerateToString: generateToString,
				AddCustomValidation: addCustomValidation,
				ProhibitParameterlessConstruction: prohibitParameterlessConstruction,
				GenerateEmptyStatic: generateEmptyStatic,
				MinimumCount: attribute.TryGetArgument<int>("minimumCount", out var minimumCount) && minimumCount != Int32.MinValue ? minimumCount : null,
				MaximumCount: attribute.TryGetArgument<int>("maximumCount", out var maximumCount) && maximumCount != Int32.MinValue ? maximumCount : null);
		return null;

		static string GetDeclaration(TypeDeclarationSyntax declaration, string name)
		{
			var declarationText = declaration.ToFullString();

			var start = declarationText.IndexOf(']') + 1;
			var end = declarationText.IndexOf(name, StringComparison.Ordinal);
			
			declarationText = end == -1
				? declarationText.Substring(start)
				: declarationText.Substring(start, end - start).Trim();
			
			return $"{declarationText} {name}";
		}
	}
}