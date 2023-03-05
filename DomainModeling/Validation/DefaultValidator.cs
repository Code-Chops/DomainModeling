namespace CodeChops.DomainModeling.Validation;

/// <summary>
/// <inheritdoc cref="Validator"/>
/// <para>Throws when a guard invalidates (default).</para>
/// <para>This validator is immutable.</para>
/// </summary>
public sealed record DefaultValidator : Validator
{
	/// <summary>
	/// Is always true, because it throws when it encounters an exception.
	/// </summary>
	public override bool IsValid => true;
	
	/// <summary>
	/// Only use this constructor if the object to be validated is a ref struct.
	/// </summary>
	public DefaultValidator(string objectName) 
		: base(objectName)
	{
	}
	
	/// <summary>
	/// Throws because of invalidation.
	/// </summary>
	public override TReturn Throw<TException, TReturn>(TException exception)
	{
		exception.Throw();

		return default!;
	}
}
