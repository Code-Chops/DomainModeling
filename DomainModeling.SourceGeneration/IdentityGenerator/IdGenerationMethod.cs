namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.IdentityGenerator;

public enum IdGenerationMethod
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