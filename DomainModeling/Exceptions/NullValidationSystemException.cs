namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class NullValidationSystemException : SystemException<NullValidationSystemException>, ISystemException<NullValidationSystemException>
{
	public static string ErrorMessage => "Required data is missing.";

	public static NullValidationSystemException Create(object parameters)
		=> new(parameters);

	public NullValidationSystemException(object parameters) 
		: base(parameters)
	{
	}
}