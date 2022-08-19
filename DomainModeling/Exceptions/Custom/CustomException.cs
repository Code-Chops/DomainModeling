namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public abstract class CustomException<TException> : Exception
	where TException : CustomException<TException>, ICustomException<TException>
{
	protected CustomException(object? parameters)
		: base(EasyStringHelper.ToExceptionString(typeof(TException), TException.ErrorMessage, parameters))
	{
	}
}