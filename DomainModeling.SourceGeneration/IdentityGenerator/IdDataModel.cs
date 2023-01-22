namespace CodeChops.DomainModeling.SourceGeneration.IdentityGenerator;

public record IdDataModel(
	string? Namespace,
	string IdTypeName,
	string UnderlyingTypeFullName,
	char? NullOperator);
