namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public interface ICustomException
{
	TReturn Throw<TReturn>();
	
	void Throw();
}
