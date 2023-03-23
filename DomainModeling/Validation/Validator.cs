namespace CodeChops.DomainModeling.Validation;

/// <summary>
/// Provides a way to easily use validation in different ways and enables the usage of <see cref="Guards.Core.IGuard"/>s.
/// </summary>
public abstract record Validator
{
	public string ObjectName { get; }
	public abstract bool HasException { get; }
	
	protected Validator(string objectName)
	{
		this.ObjectName = objectName;
	}
	
	// ReSharper disable twice ExplicitCallerInfoArgument
	public void Throw<TException>(TException exception)
		where TException : CustomException
		=> this.Throw<TException, int>(exception);

	/// <summary>
	/// <inheritdoc cref="Throw{TException}"/>
	/// <para>Returns the default of the provided return type (so it can be used in an inline if-else).</para>
	/// </summary>
	public abstract TReturn Throw<TException, TReturn>(TException exception)
		where TException : CustomException;
	
	public static class Get<TObject>
	{
		public static DefaultValidator Default { get; } = new(objectName: typeof(TObject).Name);
		public static AggregateValidator Aggregate(bool throwWhenDisposed = true) => new(objectName: typeof(TObject).Name, throwWhenDisposed);
		public static IgnoreValidator Ignore { get; } = new(objectName: typeof(TObject).Name);
	}
}
