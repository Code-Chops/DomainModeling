namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System.Core;

public abstract record SystemException<TSelf, TParameter>(string Message) : ISystemException
	where TSelf : SystemException<TSelf, TParameter>
{	
	[DoesNotReturn]
	public TReturn Throw<TReturn>(
		TParameter parameter, 
		[CallerMemberName] string? callerMemberName = null, 
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumberAttribute] int lineNumber = 0)
	{
		// ReSharper disable twice ExplicitCallerInfoArgument
		this.Throw(
			parameter: parameter, 
			callerMemberName: callerMemberName,
			callerFilePath: callerFilePath,
			lineNumber: lineNumber);

		return default;
	}

	[DoesNotReturn]
	public TParameter Throw(
		TParameter parameter, 
		[CallerMemberName] string? callerMemberName = null, 
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumberAttribute] int lineNumber = 0)
	{
		throw new Exception(
			parameter: parameter, 
			message: this.Message,
			extraText: $"caller: '{callerMemberName}, file: {callerFilePath} (R:{lineNumber})'");
	}

	private class Exception : global::System.Exception
	{
		internal Exception(TParameter parameter, string message, string extraText)
			: base(message: $"{message}. Info: {DisplayStringExtensions.GetParametersDisplayString(parameters: parameter, extraText: extraText, jsonSerializerOptions: null)}")
		{
		}
	}
}