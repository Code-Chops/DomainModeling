namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class ArgumentNullException : CustomException, ICustomException
{
	public static string ErrorMessage => nameof(ArgumentNullException);

	public static dynamic Throw(string errorMessage)
		=> throw new ArgumentException(errorMessage);

	protected ArgumentNullException(string errorMessage) 
		: base(errorMessage)
	{
	}
}