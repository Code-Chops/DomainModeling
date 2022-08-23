namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public abstract class SystemException<TException> : Exception 
	where TException : SystemException<TException>, ISystemException<TException>
{
	protected SystemException(object parameters)
		: base(message: $"{TException.ErrorMessage}. Info: {EasyStringHelper.ToDisplayString(typeof(TException), parameters, extraText: null)}")	
	{
	}
}