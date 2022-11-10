namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public enum ValidatorMode
{
	/// <summary>
	/// Throws when a guard invalidates (default).
	/// <p>The validator is immutable in this mode.</p>
	/// </summary>
	Throw,
	
	/// <summary>
	/// Collects the exceptions when a guard invalidates, but does not throw.
	/// <p>The validator is mutable in this mode.</p>
	/// </summary>
	DoNotThrow,
	
	/// <summary>
	/// Ignores when a guard invalidates. Does not collect exceptions.
	/// <p>The validator is immutable in this mode.</p>
	/// </summary>
	Ignore,
}
