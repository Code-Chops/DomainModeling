namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System.Core;

/// <summary>
/// <inheritdoc cref="ISystemException"/>
/// </summary>
/// <param name="Message">The exception message.</param>
/// <typeparam name="TSelf">The type of the concrete system exception.</typeparam>
/// <typeparam name="TParameter">The type of the parameter.</typeparam>
public abstract record SystemException<TSelf, TParameter>(string Message) : ISystemException
	where TSelf : SystemException<TSelf, TParameter>
{	
	/// <summary>
	/// Throws the exception. Returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
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

	/// <summary>
	/// Throws the exception.
	/// </summary>
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

	/// <summary>
	/// The real System.Exception is wrapped so records can be used for exceptions (records provide less boilerplate).
	/// </summary>
	private class Exception : global::System.Exception
	{
		internal Exception(TParameter parameter, string message, string extraText)
			: base(message: $"{message}. Info: {DisplayStringExtensions.GetParametersDisplayString(parameters: parameter, extraText: extraText, jsonSerializerOptions: JsonSerialization.DefaultDisplayStringOptions)}")
		{
		}
	}
}