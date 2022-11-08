using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;

/// <summary>
/// <inheritdoc cref="ISystemException"/>
/// </summary>
// ReSharper disable once UnusedTypeParameter
public class SystemException<TGuard> : CustomException, ISystemException
	where TGuard : IGuard
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
	public SystemException(ExceptionMessage<TGuard> message, Exception? innerException = null,
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
	
	public SystemException(string message, Exception? innerException = null,
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
}
