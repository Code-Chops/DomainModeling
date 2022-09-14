namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class NullValidationSystemException<TParameter> : SystemException<NullValidationSystemException<TParameter>, TParameter>, ISystemException<NullValidationSystemException<TParameter>, TParameter>
{
	public static string ErrorMessage => $"Required data for {typeof(TParameter).Name} is missing.";

	public static NullValidationSystemException<TParameter> Create(TParameter parameter)
		=> new(parameter);

	public NullValidationSystemException(TParameter parameter) 
		: base(parameter)
	{
	}
}