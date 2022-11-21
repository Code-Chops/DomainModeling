using CodeChops.SourceGeneration.Utilities.Extensions;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.IdentityGenerator;

internal static class IdSyntaxReceiver
{
	public static bool CheckIfProbablyNeedsStronglyTypedId(SyntaxNode node, CancellationToken cancellationToken)
	{
		if (node is not ClassDeclarationSyntax and not RecordDeclarationSyntax)
			return false;
		
		var attribute = ((TypeDeclarationSyntax)node).AttributeLists
			.SelectMany(list => list.Attributes)
			.SingleOrDefault(attribute => attribute.Name.HasAttributeName(IdGenerator.AttributeName, cancellationToken));

		return attribute is not null;
	}

	public static IdDataModel? GetModel(TypeDeclarationSyntax typeDeclarationSyntax, SemanticModel semanticModel)
	{
		var type = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax);
		
		if (type is null || type.IsStatic || type.TypeKind != TypeKind.Class || !typeDeclarationSyntax.Modifiers.Any(m =>  m.IsKind(SyntaxKind.PartialKeyword)))
			return null;
		
		// Check for attribute with explicit integral type.
		if (!type.HasAttribute(IdGenerator.AttributeName, IdGenerator.AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 1))
			// Check for attribute without an explicit integral type.
			if (!type.HasAttribute(IdGenerator.AttributeName, IdGenerator.AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0))
				return null;

		if (attribute is null) return null;
		
		var @namespace = type.ContainingNamespace!.IsGlobalNamespace 
			? null 
			: type.ContainingNamespace.ToDisplayString();
		var isEntityBase = type.Name == IdGenerator.EntityName && @namespace == IdGenerator.EntityNamespace;

		var idTypeName = attribute.GetArgumentOrDefault("name", IdGenerator.DefaultIdTypeName)!;
		var idPropertyName = attribute.GetArgumentOrDefault("propertyName", IdGenerator.DefaultIdPropertyName)!;
		var (primitiveType, primitiveTypeNamespace) = GetTypeNames(attribute);
		
		var data = new IdDataModel(
			OuterClassName: type.Name,
			OuterClassGenericTypeParameters: typeDeclarationSyntax.TypeParameterList?.ToFullString().Trim(),
			Namespace: @namespace, 
			OuterClassDeclaration: type.GetObjectDeclaration(),
			IdTypeName: idTypeName,
			IdPropertyName: idPropertyName,
			PrimitiveType: primitiveType,
			PrimitiveTypeNamespace: primitiveTypeNamespace,
			IdGenerationMethod: GetClassType(type, isEntityBase),
			NullOperator: type.NullableAnnotation is NullableAnnotation.Annotated && type.TypeKind is not TypeKind.Struct ? '?' : null);
		
		return data;
	}

	private static (string PrimitiveType, string? PrimitiveTypeNamespace) GetTypeNames(AttributeData attribute)
	{
		var primitiveType = IdGenerator.DefaultIdPrimitiveType;
		var primitiveTypeNamespace = (string?)null;
		
		// Get the primitive type using the generic parameter of the attribute. 
		var genericParameterName = attribute.AttributeClass?.TypeArguments.SingleOrDefault();
		if (genericParameterName is not null)
		{
			primitiveType = genericParameterName.GetTypeNameWithGenericParameters();

			primitiveTypeNamespace = genericParameterName.ContainingNamespace.ToDisplayString();
			
			if (genericParameterName.ContainingNamespace.IsGlobalNamespace || primitiveTypeNamespace == "System")
				primitiveTypeNamespace = null;
		}

		return (primitiveType, primitiveTypeNamespace);
	}

	private static IdGenerationMethod GetClassType(INamedTypeSymbol type, bool isEntityBase)
	{
		if (isEntityBase)
			return IdGenerationMethod.EntityBase;

		if (type.IsOrInheritsClass(interf => interf.IsType(IdGenerator.EntityName, IdGenerator.EntityNamespace), out _))
			return IdGenerationMethod.EntityImplementation;
			
		return type.IsRecord 
			? IdGenerationMethod.Record 
			: IdGenerationMethod.Class;
	}
}
