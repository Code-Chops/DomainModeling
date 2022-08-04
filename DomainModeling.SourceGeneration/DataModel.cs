namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration;

public enum GenerationMethod
{
	/// <summary>
	/// Generate an abstract ID-property and don't create equals.
	/// </summary>
	EntityBase,
	/// <summary>
	/// Generate an override ID-property and create equals.
	/// </summary>
	EntityImplementation,
	/// <summary>
	/// Generate a new ID-property but don't create equals.
	/// </summary>
	Record,
	/// <summary>
	/// Generate a new ID-property and create equals.
	/// </summary>
	Class,
}

public record DataModel(string Name, string? Namespace, string Declaration, string IdIntegralType, GenerationMethod GenerationMethod)
{
	public string Name { get; } = Name;
	public string? Namespace { get; } = Namespace;
	public string Declaration { get; } = Declaration;
	public string IdIntegralType { get; } = IdIntegralType;
	public GenerationMethod GenerationMethod { get; } = GenerationMethod;
}