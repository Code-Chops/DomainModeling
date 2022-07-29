namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration;

internal static class SyntaxReceiver
{
	private const string AttributeNamespace = "CodeChops.DomainDrivenDesign.DomainModeling.Attributes";
	private const string AttributeName = "GenerateEntityId";
	
	public static bool CheckIfIsProbablyEntity(SyntaxNode node, CancellationToken cancellationToken)
	{
		if (node is not ClassDeclarationSyntax classDeclaration)
			return false;
		
		var attribute = classDeclaration.AttributeLists
			.SelectMany(list => list.Attributes)
			.SingleOrDefault(attribute => attribute.Name.HasAttributeName(AttributeName, cancellationToken));

		return attribute is not null;
	}

	public static EntityModel? GetEntityModel(GeneratorSyntaxContext context, CancellationToken cancellationToken)
	{
		var typeDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
		var type = context.SemanticModel.GetDeclaredSymbol(typeDeclarationSyntax);
		
		if (type is null || type.IsAbstract || type.IsStatic || type.TypeKind != TypeKind.Class || !typeDeclarationSyntax.Modifiers.Any(m =>  m.IsKind(SyntaxKind.PartialKeyword)))
			return null;
		
		// Check for attribute with explicit integral type.
		if (!type.HasAttribute(AttributeName, AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 1))
		{
			// Check for attribute without an explicit integral type.
			if (!type.HasAttribute(AttributeName, AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0))
				return null;
		}
		
		var data = new EntityModel(type, attribute!.AttributeClass!.TypeArguments.SingleOrDefault()?.Name ?? "uint");
		return data;
	}
}