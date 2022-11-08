﻿using CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

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

		var generateToString = attribute.GetArgumentOrDefault("generateToString", defaultValue: true);
		var generateComparison = attribute.GetArgumentOrDefault("generateComparison", defaultValue: true);
		var addCustomValidation = attribute.GetArgumentOrDefault("addCustomValidation", defaultValue: true);
		var constructorIsPublic = attribute.GetArgumentOrDefault("constructorIsPublic", defaultValue: true);
		var forbidParameterlessConstruction = attribute.GetArgumentOrDefault("forbidParameterlessConstruction", defaultValue: true);
		var generateStaticDefault = attribute.GetArgumentOrDefault("generateStaticDefault", defaultValue: false);
		var generateEnumerable = attribute.GetArgumentOrDefault("generateEnumerable", defaultValue: true);
		var propertyName = attribute.GetArgumentOrDefault("propertyName", defaultValue: (string?)null);
		var propertyIsPublic = attribute.GetArgumentOrDefault("propertyIsPublic", defaultValue: false);
		var allowNull = attribute.GetArgumentOrDefault("allowNull", defaultValue: false);
		var useValidationExceptions = attribute.GetArgumentOrDefault("useValidationExceptions", defaultValue: true);

		int value;
		
		if (hasDictionaryAttribute)
			return new DictionaryValueObject(
				UseValidationExceptions: useValidationExceptions,
				MinimumCount: attribute.TryGetArgument("minimumCount", out value) && value != Int32.MinValue ? value : null,
				MaximumCount: attribute.TryGetArgument("maximumCount", out value) && value != Int32.MaxValue ? value : null,
				ValueObjectType: type,
				KeyType: attribute.AttributeClass!.TypeArguments[0],
				ValueType: attribute.AttributeClass!.TypeArguments[1],
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				ConstructorIsPublic: constructorIsPublic,
				ForbidParameterlessConstruction: forbidParameterlessConstruction,
				GenerateStaticDefault: generateStaticDefault,
				GenerateEnumerable: generateEnumerable,
				PropertyName: propertyName,
				PropertyIsPublic: propertyIsPublic,
				AllowNull: allowNull);
		
		if (hasListAttribute)
			return new ListValueObject(
				UseValidationExceptions: useValidationExceptions,
				MinimumCount: attribute.TryGetArgument("minimumCount", out value) && value != Int32.MinValue ? value : null,
				MaximumCount: attribute.TryGetArgument("maximumCount", out value) && value != Int32.MaxValue ? value : null,
				ValueObjectType: type,
				Attribute: attribute,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				ConstructorIsPublic: constructorIsPublic,
				ForbidParameterlessConstruction: forbidParameterlessConstruction,
				GenerateStaticDefault: generateStaticDefault,
				GenerateEnumerable: generateEnumerable,
				PropertyName: propertyName,
				PropertyIsPublic: propertyIsPublic,
				AllowNull: allowNull);
		
		if (hasStringAttribute)
			return new StringValueObject(
				UseValidationExceptions: useValidationExceptions,
				MinimumLength: attribute.TryGetArgument("minimumLength", out value) && value != Int32.MinValue ? value : null,
				MaximumLength: attribute.TryGetArgument("maximumLength", out value) && value != Int32.MaxValue ? value : null,
				ValueObjectType: type,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				ConstructorIsPublic: constructorIsPublic,
				ForbidParameterlessConstruction: forbidParameterlessConstruction,
				GenerateStaticDefault: generateStaticDefault,
				GenerateEnumerable: generateEnumerable,
				PropertyName: propertyName,
				PropertyIsPublic: propertyIsPublic,
				AllowNull: allowNull,
				StringCaseConversion: attribute.GetArgumentOrDefault("stringCaseConversion", StringCaseConversion.NoConversion),
				StringFormat: attribute.GetArgumentOrDefault("stringFormat", StringFormat.Default),
				StringComparison: attribute.GetArgumentOrDefault("stringComparison", StringComparison.Ordinal));
		
		if (hasDefaultAttribute)
			return new DefaultValueObject(
				UseValidationExceptions: useValidationExceptions,
				MinimumValue: attribute.TryGetArgument("minimumValue", out value) && value != Int32.MinValue ? value : null,
				MaximumValue: attribute.TryGetArgument("maximumValue", out value) && value != Int32.MaxValue ? value : null,
				ValueObjectType: type,
				Attribute: attribute,
				TypeDeclarationSyntax: typeDeclarationSyntax,
				GenerateToString: generateToString,
				GenerateComparison: generateComparison,
				AddCustomValidation: addCustomValidation,
				ConstructorIsPublic: constructorIsPublic,
				ForbidParameterlessConstruction: forbidParameterlessConstruction,
				GenerateStaticDefault: generateStaticDefault,
				PropertyName: propertyName,
				PropertyIsPublic: propertyIsPublic,
				AllowNull: allowNull);

		return null;
	}
}
