namespace CodeChops.DomainModeling.SourceGeneration.IdentityGenerator;

public record IdDataModel(
	string OuterClassName, 
	string? OuterClassGenericTypeParameters, 
	string? Namespace, 
	string OuterClassDeclaration, 
	string IdTypeName, 
	string IdPropertyName, 
	string UnderlyingTypeFullName,
	IdGenerationMethod IdGenerationMethod,
	char? NullOperator);
