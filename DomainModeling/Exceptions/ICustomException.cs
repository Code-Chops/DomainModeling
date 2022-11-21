namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public interface ICustomException
{
	string Message { get; }
	
	Exception? InnerException { get; }
	
	TReturn Throw<TReturn>();
	
	void Throw();
}
