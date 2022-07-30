namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.Models;

public enum ClassType
{
	EntityBase,
	EntityImplementation,
	Other,
}

public record DataModel(string Name, string? Namespace, string Declaration, string IdIntegralType, ClassType ClassType)
{
	public string Name { get; } = Name;
	public string? Namespace { get; } = Namespace;
	public string Declaration { get; } = Declaration;
	public string IdIntegralType { get; } = IdIntegralType;
	public ClassType ClassType { get; } = ClassType;
}