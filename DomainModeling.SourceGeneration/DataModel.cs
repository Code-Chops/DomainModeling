namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration;

public record DataModel(string Name, string? GenericTypeParameters, string? Namespace, string Declaration, string IdIntegralType, GenerationMethod GenerationMethod);