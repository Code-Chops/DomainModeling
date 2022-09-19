namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.IdentityGenerator;

public record IdDataModel(
	string OuterClassName, 
	string? OuterClassGenericTypeParameters, 
	string? Namespace, 
	string OuterClassDeclaration, 
	string IdTypeName, 
	string IdPropertyName, 
	string IdPrimitiveType,
	string? PrimitiveTypeNamespace,
	string IdBaseType, 
	IdGenerationMethod IdGenerationMethod);