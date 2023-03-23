namespace CodeChops.DomainModeling.Validation;

/// <summary>
/// <para>Ignores when a guard invalidates. Does not collect exceptions and always thinks it's valid.</para>
/// <para>Only use for optimisation purposes!</para>
/// <para>In case a guard invalidates and should return an object, it returns a default.</para>
/// <para>This validator is immutable.</para>
/// </summary>
public record IgnoreValidator : Validator
{
	/// <summary>
	/// Is always true.
	/// </summary>
	public override bool HasException => true;
	
	/// <inheritdoc />
	internal IgnoreValidator(string objectName) 
		: base(objectName)
	{
	}
	
	/// <summary>
	/// Does not throw and returns the default of <typeparam name="TReturn"></typeparam>.
	/// </summary>
	public override TReturn Throw<TException, TReturn>(TException exception)
	{
		return default!;
	}
}
