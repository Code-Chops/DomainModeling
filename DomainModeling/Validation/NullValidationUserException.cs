namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public class NullValidationUserException : UserException<NullValidationUserException>, IUserException<NullValidationUserException>
{
	public static string ErrorMessage => "Required data is missing.";

	public static NullValidationUserException Create(Id<string> errorCode, string parameterName)
		=> new(new UserErrorMessage(errorCode, parameterName));

	protected NullValidationUserException(UserErrorMessage errorMessage) 
		: base(errorMessage)
	{
	}
}
