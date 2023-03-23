namespace CodeChops.DomainModeling.Exceptions;

public class CustomAggregateException : AggregateException
{
	internal CustomAggregateException(IEnumerable<CustomException> exceptions)
		: base(exceptions)
	{
	}
	
	/// <summary>
	/// Throws the exception. Returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
	[DoesNotReturn]
	public TReturn Throw<TReturn>() => throw this;

	/// <summary>
	/// Throws the exception.
	/// </summary>
	[DoesNotReturn]
	public void Throw() => throw this;
}
