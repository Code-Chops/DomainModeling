namespace CodeChops.DomainModeling.SourceGeneration.IdentityGenerator;

public record IdDataModel(
	string? Namespace,
	string Name,
	string UnderlyingTypeName,
	char? NullOperator);
