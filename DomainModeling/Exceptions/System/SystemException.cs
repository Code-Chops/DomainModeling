using CodeChops.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainModeling.Exceptions.System;

/// <summary>
/// An exception that contains system-information and therefore should not be visible to the user.
/// </summary>
// ReSharper disable once UnusedTypeParameter
// ReSharper disable ExplicitCallerInfoArgument
public class CustomSystemException<TGuard> : CustomSystemException
	where TGuard : IGuard
{
	public CustomSystemException(ValidationExceptionMessage message, Exception? innerException = null, string? callerMemberName = null, string? callerFilePath = null, int? callerLineNumber = null) 
		: base(message, innerException, callerMemberName, callerFilePath, callerLineNumber)
	{
	}

	public CustomSystemException(string message, Exception? innerException = null, string? callerMemberName = null, string? callerFilePath = null, int? callerLineNumber = null) 
		: base(message, innerException, callerMemberName, callerFilePath, callerLineNumber)
	{
	}
}

public class CustomSystemException : CustomException
{
		/// <summary>
	/// The name of the member in which the exception occurred.
	/// </summary>
	public string? MemberName { get; }

	/// <summary>
	/// The path of the file in which the exception occurred.
	/// </summary>
	public string? FilePath { get; }
	
	/// <summary>
	/// The line number of the file in which the exception occurred.
	/// </summary>
	public int? LineNumber { get; }
	
	// ReSharper disable ExplicitCallerInfoArgument
	public CustomSystemException(ValidationExceptionMessage message, Exception? innerException = null,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
		: this(
			message: message.ToString(), 
			innerException: innerException, 
			callerMemberName: callerMemberName, 
			callerFilePath: callerFilePath, 
			callerLineNumber: callerLineNumber)
	{
	}
	
	public CustomSystemException(string message, Exception? innerException = null,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
		: base(message: GetFullMessage(message, callerMemberName, callerFilePath, callerLineNumber), innerException: innerException)
	{
		this.MemberName = callerMemberName;
		this.FilePath = callerFilePath;
		this.LineNumber = callerLineNumber;
	}
	
	private static string GetFullMessage(string message, string? callerMemberName, string? callerFilePath, int? callerLineNumber)
	{
		message = DisplayStringExtensions.GetParametersDisplayString(
			parameters: message, 
			extraText: $"caller: '{callerMemberName}, file: {callerFilePath} (R:{callerLineNumber})'", 
			jsonSerializerOptions: JsonSerialization.DefaultDisplayStringOptions).ToString();
			
		return message;
	}
	
	/// <summary>
	/// Throws the exception. Returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
	[DoesNotReturn]
	public override TReturn Throw<TReturn>() => throw this;

	/// <summary>
	/// Throws the exception.
	/// </summary>
	[DoesNotReturn]
	public override void Throw() => throw this;	
}
