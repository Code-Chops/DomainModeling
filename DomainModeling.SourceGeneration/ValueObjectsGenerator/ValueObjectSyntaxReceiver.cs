using CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

namespace CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator;

internal static class ValueObjectSyntaxReceiver
{
	private const string AttributeNamespace			= "CodeChops.DomainModeling.Attributes.ValueObjects";
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
		
		if (type.TypeKind != TypeKind.Struct && type.TypeKind != TypeKind.Class) 
			return null;
		
		var hasDefaultAttribute = type.HasAttribute(DefaultAttributeName, AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 0)
			|| type.HasAttribute(DefaultAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 1);
		
		var hasStringAttribute = attribute is null && type.HasAttribute(StringAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0);
		
		var hasListAttribute = attribute is null && (type.HasAttribute(ListAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0)
			|| type.HasAttribute(ListAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 1));
		
		var hasDictionaryAttribute = attribute is null && (type.HasAttribute(DictionaryAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0)
			|| type.HasAttribute(DictionaryAttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 2));
		
		if (attribute is null) 
			return null;

		var generateToString = attribute.GetArgumentOrDefault("generateToString", defaultValue: true);
		var generateComparison = attribute.GetArgumentOrDefault("generateComparison", defaultValue: true);
		var generateDefaultConstructor = attribute.GetArgumentOrDefault("generateDefaultConstructor", defaultValue: true);
		var forbidParameterlessConstruction = attribute.GetArgumentOrDefault("forbidParameterlessConstruction", defaultValue: true);
		var generateStaticDefault = attribute.GetArgumentOrDefault("generateStaticDefault", defaultValue: false);
		var generateEnumerable = attribute.GetArgumentOrDefault("generateEnumerable", defaultValue: true);
		var propertyName = attribute.GetArgumentOrDefault("propertyName", defaultValue: (string?)null);
		var propertyIsPublic = attribute.GetArgumentOrDefault("propertyIsPublic", defaultValue: false);
		var valueIsNullable = attribute.GetArgumentOrDefault("valueIsNullable", defaultValue: false);
		var useValidationExceptions = attribute.GetArgumentOrDefault("useValidationExceptions", defaultValue: true);
		var useCustomProperty = attribute.GetArgumentOrDefault("useCustomProperty", defaultValue: true);

		int value;
		
		if (hasDefaultAttribute)
			return new DefaultValueObject(
				valueObjectType: type,
				providedUnderlyingType: (INamedTypeSymbol?)attribute.AttributeClass!.TypeArguments.SingleOrDefault() ?? GetTypeFromConstructor(attribute, index: 0),
				minimumValue: attribute.TryGetArgument("minimumValue", out value) && value != Int32.MinValue ? value : null,
				maximumValue: attribute.TryGetArgument("maximumValue", out value) && value != Int32.MaxValue ? value : null,
				typeDeclarationSyntax: typeDeclarationSyntax,
				generateToString: generateToString,
				generateComparison: generateComparison,
				generateDefaultConstructor: generateDefaultConstructor,
				forbidParameterlessConstruction: forbidParameterlessConstruction,
				generateStaticDefault: generateStaticDefault,
				propertyName: propertyName,
				propertyIsPublic: propertyIsPublic,
				allowNull: valueIsNullable,
				useValidationExceptions: useValidationExceptions,
				useCustomProperty: useCustomProperty);
		
		if (hasDictionaryAttribute)
			return new DictionaryValueObject(
				valueObjectType: type,
				providedKeyType: (INamedTypeSymbol?)attribute.AttributeClass!.TypeArguments.ElementAtOrDefault(0) ?? GetTypeFromConstructor(attribute, index: 0),
				providedValueType: (INamedTypeSymbol?)attribute.AttributeClass!.TypeArguments.ElementAtOrDefault(1) ?? GetTypeFromConstructor(attribute, index: 1),
				minimumCount: attribute.TryGetArgument("minimumCount", out value) && value != Int32.MinValue ? value : null,
				maximumCount: attribute.TryGetArgument("maximumCount", out value) && value != Int32.MaxValue ? value : null,
				generateEnumerable: generateEnumerable,
				generateToString: generateToString,
				generateComparison: generateComparison,
				generateDefaultConstructor: generateDefaultConstructor,
				forbidParameterlessConstruction: forbidParameterlessConstruction,
				generateStaticDefault: generateStaticDefault,
				propertyName: propertyName,
				propertyIsPublic: propertyIsPublic,
				allowNull: valueIsNullable,
				useValidationExceptions: useValidationExceptions);
		
		if (hasListAttribute)
			return new ListValueObject(
				valueObjectType: type,
				providedElementType: (INamedTypeSymbol?)attribute.AttributeClass!.TypeArguments.SingleOrDefault() ?? GetTypeFromConstructor(attribute, index: 0),
				minimumCount: attribute.TryGetArgument("minimumCount", out value) && value != Int32.MinValue ? value : null,
				maximumCount: attribute.TryGetArgument("maximumCount", out value) && value != Int32.MaxValue ? value : null,
				generateToString: generateToString,
				generateComparison: generateComparison,
				generateDefaultConstructor: generateDefaultConstructor,
				forbidParameterlessConstruction: forbidParameterlessConstruction,
				generateStaticDefault: generateStaticDefault,
				generateEnumerable: generateEnumerable,
				propertyName: propertyName,
				propertyIsPublic: propertyIsPublic,
				allowNull: valueIsNullable,
				useValidationExceptions: useValidationExceptions);
		
		if (hasStringAttribute)
			return new StringValueObject(
				ValueObjectType: type,
				MinimumLength: attribute.TryGetArgument("minimumLength", out value) && value != Int32.MinValue ? value : null,
				MaximumLength: attribute.TryGetArgument("maximumLength", out value) && value != Int32.MaxValue ? value : null,
				StringCaseConversion: attribute.GetArgumentOrDefault("stringCaseConversion", StringCaseConversion.NoConversion),
				StringFormat: attribute.GetArgumentOrDefault("stringFormat", StringFormat.Default),
				StringComparison: attribute.GetArgumentOrDefault("stringComparison", StringComparison.Ordinal),
				UseRegex: attribute.GetArgumentOrDefault<bool>("useRegex"),
				GenerateEnumerable: generateEnumerable,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				GenerateDefaultConstructor: generateDefaultConstructor,
				ForbidParameterlessConstruction: forbidParameterlessConstruction,
				GenerateStaticDefault: generateStaticDefault,
				PropertyName: propertyName,
				PropertyIsPublic: propertyIsPublic,
				AllowNull: valueIsNullable,
				UseValidationExceptions: useValidationExceptions);

		return null;

		
		static INamedTypeSymbol? GetTypeFromConstructor(AttributeData attribute, int index = 0) 
			=> (INamedTypeSymbol?)attribute.ConstructorArguments.Skip(index).FirstOrDefault(arg => arg.Type?.IsType<Type>() ?? false).Value;
	}
}
