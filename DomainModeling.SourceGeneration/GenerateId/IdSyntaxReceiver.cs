namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.GenerateId;

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
			.SingleOrDefault(attribute => attribute.Name.HasAttributeName(SourceBuilder.AttributeName, cancellationToken));

		return attribute is not null;
	}

	public static DataModel? GetModel(TypeDeclarationSyntax typeDeclarationSyntax, SemanticModel semanticModel)
	{
		var type = semanticModel.GetDeclaredSymbol(typeDeclarationSyntax);
		
		if (type is null || type.IsStatic || type.TypeKind != TypeKind.Class || !typeDeclarationSyntax.Modifiers.Any(m =>  m.IsKind(SyntaxKind.PartialKeyword)))
			return null;
		
		// Check for attribute with explicit integral type.
		if (!type.HasAttribute(SourceBuilder.AttributeName, SourceBuilder.AttributeNamespace, out var attribute, expectedGenericTypeParamCount: 1))
		{
			// Check for attribute without an explicit integral type.
			if (!type.HasAttribute(SourceBuilder.AttributeName, SourceBuilder.AttributeNamespace, out attribute, expectedGenericTypeParamCount: 0))
				return null;
		}

		if (attribute is null) return null;
		
		var @namespace = type.ContainingNamespace!.IsGlobalNamespace 
			? null 
			: type.ContainingNamespace.ToDisplayString();
		var isEntityBase = type.Name == SourceBuilder.EntityName && @namespace == SourceBuilder.EntityNamespace;

		var idTypeName = GetIdTypeName(attribute, type);
		var idPropertyName = GetIdPropertyName(attribute, type);
		var (baseType, primitiveType) = GetTypeNames(attribute, type, idTypeName);
		
		var data = new DataModel(
			OuterClassName: type.Name,
			OuterClassGenericTypeParameters: typeDeclarationSyntax.TypeParameterList?.ToFullString(),
			Namespace: @namespace, 
			OuterClassDeclaration: type.GetObjectDeclaration(),
			IdTypeName: idTypeName,
			IdPropertyName: idPropertyName,
			IdPrimitiveType: primitiveType,
			IdBaseType: baseType,
			GenerationMethod: GetClassType(type, isEntityBase));
		
		return data;
	}

	private static string GetIdTypeName(AttributeData attribute, INamedTypeSymbol type)
	{
		var idName = SourceBuilder.DefaultIdTypeName;
		if (attribute.TryGetArguments(out var argumentConstantByNames) && argumentConstantByNames!.TryGetValue("name", out var providedIdName) && providedIdName.Value is not null)
		{
			if (providedIdName.Value is not string value)
				throw new InvalidCastException($"Unable to cast value of \"name\" to string, from attribute for {attribute.AttributeClass?.Name} of class {type.Name}.");

			idName = value;
		}

		return idName;
	}
	
	private static string GetIdPropertyName(AttributeData attribute, INamedTypeSymbol type)
	{
		var idPropertyName = SourceBuilder.DefaultIdPropertyName;
		if (attribute.TryGetArguments(out var argumentConstantByNames) && argumentConstantByNames!.TryGetValue("propertyName", out var providedIdName) && providedIdName.Value is not null)
		{
			if (providedIdName.Value is not string value)
				throw new InvalidCastException($"Unable to cast value of \"propertyName\" to string, from attribute for {attribute.AttributeClass?.Name} of class {type.Name}.");

			idPropertyName = value;
		}

		return idPropertyName;
	}

	private static (string BaseType, string PrimitiveType) GetTypeNames(AttributeData attribute, INamedTypeSymbol type, string idName)
	{
		var primitiveType = SourceBuilder.DefaultIdPrimitiveType;
		
		// Get the primitive type using the generic parameter of the attribute. 
		var genericParameterName = attribute.AttributeClass?.TypeArguments.SingleOrDefault();
		if (genericParameterName is not null)
			primitiveType = genericParameterName.GetTypeNameWithGenericParameters();

		// Get the primitive type as provided in the constructor of the attribute.
		if (attribute.TryGetArguments(out var argumentConstantByNames) && argumentConstantByNames!.TryGetValue("baseType", out var providedBaseType) && providedBaseType.Value is not null)
		{
			if (providedBaseType.Value is not ITypeSymbol value)
				throw new InvalidCastException($"Unable to cast value of \"baseType\" to {nameof(ITypeSymbol)}, from attribute for {attribute.AttributeClass?.Name} of class {type.Name}.");

			var baseType = value.GetTypeNameWithGenericParameters().Replace(" ", "");
			baseType = baseType.Replace("<>", $"<{idName}>");
			baseType = baseType.Replace("<,>", $"<{idName}, {primitiveType}>");
			
			return (baseType, primitiveType);
		}
		
		return ($"Id<{idName}, {primitiveType}>", primitiveType);
	}

	private static GenerationMethod GetClassType(INamedTypeSymbol type, bool isEntityBase)
	{
		if (isEntityBase)
			return GenerationMethod.EntityBase;

		if (type.IsOrInheritsClass(interf => interf.IsType(SourceBuilder.EntityName, SourceBuilder.EntityNamespace), out _))
			return GenerationMethod.EntityImplementation;

		if (type.IsRecord)
			return GenerationMethod.Record;
			
		return GenerationMethod.Class;
	}
}