using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public abstract class SystemException<TException, TParameter> : Exception 
	where TException : SystemException<TException, TParameter>, ISystemException<TException, TParameter>
{
	protected SystemException(TParameter parameter, string? extraText = null, JsonSerializerOptions? jsonSerializerOptions = null!)
		: base(message: $"{TException.ErrorMessage}. Info: {DisplayStringHelper.ToDisplayString<object>(parameter, extraText: extraText, jsonSerializerOptions)}")	
	{
	}
}