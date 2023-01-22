namespace CodeChops.DomainModeling.SourceGeneration.IdentityGenerator;

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
		
		if (type is null || type.IsStatic || type.TypeKind != TypeKind.Class)
			return null;
		
		// Check for attribute with explicit underlying type.
		if (!type.HasAttribute(IdGenerator.AttributeName, IdGenerator.AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 1))
			// Check for attribute without an explicit underlying type.
			if (!type.HasAttribute(IdGenerator.AttributeName, IdGenerator.AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0))
				return null;

		if (attribute is null) 
			return null;
		
		var @namespace = type.ContainingNamespace!.IsGlobalNamespace 
			? null 
			: type.ContainingNamespace.ToDisplayString();

		var idTypeName = attribute.GetArgumentOrDefault("name", type.Name + IdGenerator.DefaultIdPropertyName)!;

		var underlyingType = attribute.AttributeClass?.TypeArguments.SingleOrDefault();

		var nullOperator = underlyingType?.TypeKind is TypeKind.Class ? '?' : (char?)null;
		
		var underlyingTypeFullName = underlyingType is null 
			? IdGenerator.DefaultIdUnderlyingType 
			: underlyingType.GetFullTypeNameWithGenericParameters() + nullOperator;
		
		var data = new IdDataModel(
			Namespace: @namespace,
			IdTypeName: idTypeName,
			UnderlyingTypeFullName: underlyingTypeFullName,
			NullOperator: nullOperator);
		
		return data;
	}
}
