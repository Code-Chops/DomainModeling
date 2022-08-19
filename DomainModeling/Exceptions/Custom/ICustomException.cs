namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public interface ICustomException
{
	public static abstract dynamic Throw(string errorMessage);
	public static abstract string ErrorMessage { get; }
}