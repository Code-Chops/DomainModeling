namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public abstract class SystemException<TException, TParameter> : Exception 
	where TException : SystemException<TException, TParameter>, ISystemException<TException, TParameter>
{
	protected SystemException(TParameter key, string? extraText = null)
		: base(message: $"{TException.ErrorMessage}. Info: {EasyStringHelper.ToDisplayString<TException>(key, extraText: extraText)}")	
	{
	}
}