namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration;

internal static class SyntaxReceiver
{
	public static bool CheckIfProbablyNeedsStronglyTypedId(SyntaxNode node, CancellationToken cancellationToken)
	{
		if (node is not TypeDeclarationSyntax typeDeclarationSyntax)
			return false;
		
		if (typeDeclarationSyntax is not ClassDeclarationSyntax and not RecordDeclarationSyntax)
			return false;
		
		var attribute = typeDeclarationSyntax.AttributeLists
			.SelectMany(list => list.Attributes)
			.SingleOrDefault(attribute => attribute.Name.HasAttributeName(StronglyTypedIdGenerator.AttributeName, cancellationToken));

		return attribute is not null;
	}

	public static DataModel? GetModel(GeneratorSyntaxContext context, CancellationToken cancellationToken)
	{
		var typeDeclarationSyntax = (TypeDeclarationSyntax)context.Node;
		var type = context.SemanticModel.GetDeclaredSymbol(typeDeclarationSyntax);
		
		if (type is null || type.IsStatic || type.TypeKind != TypeKind.Class || !typeDeclarationSyntax.Modifiers.Any(m =>  m.IsKind(SyntaxKind.PartialKeyword)))
			return null;
		
		// Check for attribute with explicit integral type.
		if (!type.HasAttribute(StronglyTypedIdGenerator.AttributeName, StronglyTypedIdGenerator.AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 1))
		{
			// Check for attribute without an explicit integral type.
			if (!type.HasAttribute(StronglyTypedIdGenerator.AttributeName, StronglyTypedIdGenerator.AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0))
				return null;
		}

		var @namespace = type.ContainingNamespace!.IsGlobalNamespace 
			? null 
			: type.ContainingNamespace.ToDisplayString();
		var isEntityBase = type.Name == StronglyTypedIdGenerator.EntityName && @namespace == StronglyTypedIdGenerator.EntityNamespace;

		var data = new DataModel(
			Name: type.Name,
			GenericTypeParameters: typeDeclarationSyntax.TypeParameterList?.ToFullString(),
			Namespace: @namespace, 
			Declaration: type.GetObjectDeclaration(),
			IdIntegralType: attribute!.AttributeClass!.TypeArguments.SingleOrDefault()?.Name ?? "ulong",
			GenerationMethod: GetClassType(type, isEntityBase));
		
		return data;
		
		static GenerationMethod GetClassType(INamedTypeSymbol type, bool isEntityBase)
		{
			if (isEntityBase)
				return GenerationMethod.EntityBase;

			if (type.IsOrInheritsClass(interf => interf.IsType(StronglyTypedIdGenerator.EntityName, StronglyTypedIdGenerator.EntityNamespace), out _))
				return GenerationMethod.EntityImplementation;

			if (type.IsRecord)
				return GenerationMethod.Record;
			
			return GenerationMethod.Class;
		}
	}
}