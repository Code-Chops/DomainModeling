namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class CustomException : Exception
{
	protected CustomException(string errorMessage) 
		: base(errorMessage)
	{
	}
}