namespace CodeChops.DomainModeling.Validation;

public enum ValidatorMode
{
	/// <summary>
	/// <para>Throws when a guard invalidates (default).</para>
	/// <para>The validator is immutable in this mode.</para>
	/// </summary>
	Default,
	
	/// <summary>
	/// <para>Collects the exceptions when a guard invalidates, but does not throw.</para>
	/// <para>In case a guard invalidates and should return an object, it returns a default.</para>
	/// <para>The validator is mutable in this mode.</para>
	/// </summary>
	DoNotThrow,
	
	/// <summary>
	/// <para>Ignores when a guard invalidates. Does not collect exceptions and always thinks it's valid.</para>
	/// <para>Only use for optimisation purposes!</para>
	/// <para>In case a guard invalidates and should return an object, it returns a default.</para>
	/// <para>The validator is immutable in this mode.</para>
	/// </summary>
	Oblivious,
}
