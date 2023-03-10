namespace CodeChops.DomainModeling.Exceptions;

public abstract class CustomException : Exception
{
	public abstract TReturn Throw<TReturn>();
	
	public abstract void Throw();

	protected CustomException(string message, Exception? innerException = null)
		: base(message, innerException)
	{
	}
}
