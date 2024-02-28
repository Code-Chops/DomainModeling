// ReSharper disable ExplicitCallerInfoArgument

namespace CodeChops.DomainModeling.Validation.Guards.Core;

public interface IGuard<out TSelf, in TMessageParam> : IGuard<TSelf>
	where TSelf : IGuard<TSelf, TMessageParam>, IHasExceptionMessage<TSelf, TMessageParam>
{
	public static TReturn ThrowException<TReturn>(string objectName, TMessageParam messageParameter, object? errorCode, Exception? innerException = null,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
	{
		var message = GetMessage(objectName, messageParameter);
		var exception = CreateException(message, errorCode, innerException, callerMemberName, callerFilePath, callerLineNumber);
		
		return exception.Throw<TReturn>();
	}
	
	public static ValidationExceptionMessage GetMessage<TObject>(TMessageParam messageParameter)
		where TObject : IDomainObject
		=> GetMessage(typeof(TObject).Name, messageParameter);
	
	public static ValidationExceptionMessage GetMessage(string objectName, TMessageParam messageParameter)
	{
		var message = TSelf.GetExceptionMessage(objectName, messageParameter);
		
		return messageParameter is ITuple tuple
			? new ValidationExceptionMessage(objectName, message, tuple.GetEnumerable())
			: new ValidationExceptionMessage(objectName, message, messageParameter);
	}
}

public interface IGuard<out TSelf> : IGuard
	where TSelf : IGuard<TSelf>
{
	public static CustomException CreateException(ValidationExceptionMessage message, object? errorCode, Exception? innerException = null,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
	{
		return errorCode is null
			? new CustomSystemException<TSelf>(message, innerException, callerMemberName, callerFilePath, callerLineNumber)
			: new ValidationException<TSelf>(errorCode, message, innerException);
	}
}

/// <summary>
/// <para>
/// A guard contains domain object validation logic and an exception message.
/// This way validation checks can be re-used and exception messages are consistent.
/// Consistency of messages is especially relevant when using <see cref="ValidationException"/>s.
/// </para>
/// <para>
/// Guards also help avoid the usage of the throw keyword in hot paths as throw keywords prevent JIT-inlining.
/// </para>
/// <para>
/// The two types of validation are:
/// <list type="bullet">
/// <item><see cref="NoOutputGuardBase{TSelf,TInput}"/></item>
/// <item><see cref="OutputGuardBase{TSelf,TInput,TOutput}"/></item>
/// </list>
/// </para>
/// </summary>
public interface IGuard;
