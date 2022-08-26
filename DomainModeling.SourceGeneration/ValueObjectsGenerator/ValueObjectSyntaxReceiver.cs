using System.Globalization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator;

internal static class ValueObjectSyntaxReceiver
{
	public static bool CheckIfProbablyIsValueObjectToGenerate(SyntaxNode node, CancellationToken cancellationToken)
	{
		if (node is not RecordDeclarationSyntax and not StructDeclarationSyntax and not ClassDeclarationSyntax)
			return false;
		
		var attribute = ((TypeDeclarationSyntax)node).AttributeLists
			.SelectMany(list => list.Attributes)
			.SingleOrDefault(attribute =>
				attribute.Name.HasAttributeName(ValueObjectGenerator.IntegralAttributeName, cancellationToken)
				|| attribute.Name.HasAttributeName(ValueObjectGenerator.StringAttributeName, cancellationToken));

		return attribute is not null;
	}

	public static ValueObject? GetModel(TypeDeclarationSyntax typeDeclarationSyntax, SemanticModel semanticModel)
	{
		var type = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax);

		if (type is null || type.IsStatic || !typeDeclarationSyntax.Modifiers.Any(m =>  m.IsKind(SyntaxKind.PartialKeyword))) 
			return null;

		if (type.TypeKind != TypeKind.Struct && type.TypeKind != TypeKind.Class) return null;
		
		if (type.HasAttribute(ValueObjectGenerator.IntegralAttributeName, ValueObjectGenerator.AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 1))
		{
			// Get the primitive type using the generic parameter of the attribute. 
			var genericParameterName = attribute!.AttributeClass!.TypeArguments.Single();
			
			return new IntegralValueObject(
				Name:					type.Name, 
				Namespace:				type.ContainingNamespace!.IsGlobalNamespace ? null : type.ContainingNamespace.ToDisplayString(),
				GenerateToString:		attribute.GetArgumentOrDefault("generateToString", true),
				GenerateEmptyStatic:	attribute.GetArgumentOrDefault("generateEmptyStatic", true),
				TypeName:				genericParameterName.Name,
				Declaration:			GetDeclaration(typeDeclarationSyntax),
				MinimumValue:			attribute.TryGetArgument<int>("minimumValue", out var minimumValue) && minimumValue != Int32.MinValue ? minimumValue : null,
				MaximumValue:			attribute.TryGetArgument<int>("maximumValue", out var maximumValue) && maximumValue != Int32.MinValue ? maximumValue : null);
		}
		else if (type.HasAttribute(ValueObjectGenerator.StringAttributeName, ValueObjectGenerator.AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0))
		{
			return new StringValueObject(
				Name:					type.Name,
				Namespace:				type.ContainingNamespace!.IsGlobalNamespace ? null : type.ContainingNamespace.ToDisplayString(),
				Declaration:			GetDeclaration(typeDeclarationSyntax),
				GenerateToString:		attribute!.GetArgumentOrDefault("generateToString",		true),
				GenerateEmptyStatic:	attribute!.GetArgumentOrDefault("generateEmptyStatic",	true),
				GenerateEnumerable:		attribute!.GetArgumentOrDefault("generateEnumerable",	true),
				MinimumLength:			attribute!.TryGetArgument<int>("minimumLength", out var minimumLength) && minimumLength != Int32.MinValue ? minimumLength : null,
				MaximumLength:			attribute!.TryGetArgument<int>("maximumLength", out var maximumLength) && maximumLength != Int32.MinValue ? maximumLength : null,
				StringCaseConversion:	attribute!.GetArgumentOrDefault("stringCaseConversion",	StringCaseConversion.NoConversion),
				StringFormat:			attribute!.GetArgumentOrDefault("stringFormat",			StringFormat.Default),
				CompareOptions:			attribute!.GetArgumentOrDefault("compareOptions",		CompareOptions.None));
		}

		return null;


		static string GetDeclaration(TypeDeclarationSyntax declaration)
		{
			var declarationText = declaration.ToFullString();

			var start = declarationText.IndexOf(']') + 1;
			var end = declarationText.IndexOf('{');
			
			declarationText = end == -1
				? declarationText.Substring(start)
				: declarationText.Substring(start, end - start);
			
			return declarationText.Trim().TrimEnd(';');
		}
	}
}

/// <summary>
/// Provides extensions on <see cref="AttributeData"/>.
/// </summary>
public static class AttributeDataExtensions
{
	/// <summary>
	/// Tries to get the arguments of the attribute.
	/// </summary>
	public static bool TryGetArguments(this AttributeData attributeData, out Dictionary<string, TypedConstant>? argumentConstantByNames)
	{
		var constructorParameters = attributeData.AttributeConstructor?.Parameters;
		if (constructorParameters is null) 
		{
			argumentConstantByNames = null;
			return false; 
		}

		// Start with an indexed list of names for mandatory arguments.
		var argumentNames = constructorParameters.Value.Select(parameterSymbol => parameterSymbol.Name).ToList();

		// Combine the argument names by their constant (retrieved from the constructor arguments).
		var argumentNameAndTypePairs = attributeData.ConstructorArguments.Select((type, index) => (Name: argumentNames[index], Type: type));
		
		// Create a dictionary with the argument as key and argument constant by value.
		argumentConstantByNames = argumentNameAndTypePairs.ToDictionary(argument => argument.Name, argument => argument.Type, StringComparer.OrdinalIgnoreCase);
		
		return true;
	}
}