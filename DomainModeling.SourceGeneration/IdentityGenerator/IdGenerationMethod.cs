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
	/// Generate a new ID-property and create equals.
	/// </summary>
	Class,
	/// <summary>
	/// Generate a new ID-property and create equals.
	/// <para>Only use this in exceptional cases, for example with event contracts: they are a DTO and contain an ID.</para>
	/// </summary>
	Record,
}
