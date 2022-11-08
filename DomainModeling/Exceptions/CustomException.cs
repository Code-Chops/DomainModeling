namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class CustomException : Exception, ICustomException
{
	protected CustomException(string message, Exception? innerException)
		: base(message, innerException)
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
