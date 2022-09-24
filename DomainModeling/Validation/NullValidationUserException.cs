namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public class NullValidationUserException<TParameter> : UserException<NullValidationUserException<TParameter>, TParameter>, IUserException<NullValidationUserException<TParameter>, TParameter>
{
	public static string ErrorMessage => $"Required data for {typeof(TParameter).Name} is missing.";
	
	public static NullValidationUserException<TParameter> Create(IId<string> errorCode, TParameter parameter)
		=> new(errorCode, parameter);

	protected NullValidationUserException(IId<string> errorCode, TParameter parameter) 
		: base(new(errorCode, parameter))
	{
	}
}
