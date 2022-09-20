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
		{
			// Check for attribute without an explicit integral type.
			if (!type.HasAttribute(IdGenerator.AttributeName, IdGenerator.AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0))
				return null;
		}

		if (attribute is null) return null;
		
		var @namespace = type.ContainingNamespace!.IsGlobalNamespace 
			? null 
			: type.ContainingNamespace.ToDisplayString();
		var isEntityBase = type.Name == IdGenerator.EntityName && @namespace == IdGenerator.EntityNamespace;

		var idTypeName = attribute.GetArgumentOrDefault("name", IdGenerator.DefaultIdTypeName);
		var idPropertyName = attribute.GetArgumentOrDefault("propertyName", IdGenerator.DefaultIdPropertyName);
		var (baseType, primitiveType, primitiveTypeNamespace) = GetTypeNames(attribute, type, idTypeName);
		
		var data = new IdDataModel(
			OuterClassName: type.Name,
			OuterClassGenericTypeParameters: typeDeclarationSyntax.TypeParameterList?.ToFullString(),
			Namespace: @namespace, 
			OuterClassDeclaration: type.GetObjectDeclaration(),
			IdTypeName: idTypeName,
			IdPropertyName: idPropertyName,
			IdPrimitiveType: primitiveType,
			PrimitiveTypeNamespace: primitiveTypeNamespace,
			IdBaseType: baseType,
			IdGenerationMethod: GetClassType(type, isEntityBase));
		
		return data;
	}

	private static (string BaseType, string PrimitiveType, string? PrimitiveTypeNamespace) GetTypeNames(AttributeData attribute, INamedTypeSymbol type, string idName)
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

		// Get the primitive type as provided in the constructor of the attribute.
		if (attribute.TryGetArguments(out var argumentConstantByNames) && argumentConstantByNames!.TryGetValue("baseType", out var providedBaseType) && providedBaseType.Value is not null)
		{
			if (providedBaseType.Value is not ITypeSymbol value)
				throw new InvalidCastException($"Unable to cast value of \"baseType\" to {nameof(ITypeSymbol)}, from attribute for {attribute.AttributeClass?.Name} of class {type.Name}.");

			var baseType = value.GetTypeNameWithGenericParameters().Replace(" ", "");
			baseType = baseType.Replace("<>", $"<{idName}>");
			baseType = baseType.Replace("<,>", $"<{idName}, {primitiveType}>");
			
			return (baseType, primitiveType, primitiveTypeNamespace);
		}
		
		return ($"Id<{idName}, {primitiveType}>", primitiveType, primitiveTypeNamespace);
	}

	private static IdGenerationMethod GetClassType(INamedTypeSymbol type, bool isEntityBase)
	{
		if (isEntityBase)
			return IdGenerationMethod.EntityBase;

		if (type.IsOrInheritsClass(interf => interf.IsType(IdGenerator.EntityName, IdGenerator.EntityNamespace), out _))
			return IdGenerationMethod.EntityImplementation;

		if (type.IsRecord)
			throw new Exception($"Type {type.Name} is a record which shouldn't contain IDs.");
			
		return IdGenerationMethod.Class;
	}
}