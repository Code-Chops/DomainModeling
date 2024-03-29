// ReSharper disable ExplicitCallerInfoArgument

namespace CodeChops.DomainModeling.Validation.Guards.Core;

/// <summary>
/// <inheritdoc cref="OutputGuardBase{TSelf,TInput,TOutput,TMessageParam}"/>
/// </summary>
public abstract record OutputGuardBase<TSelf, TInput, TOutput> : OutputGuardBase<TSelf, TInput, TInput, TOutput>
	where TSelf : OutputGuardBase<TSelf, TInput, TOutput>, IOutputGuard<TInput, TOutput>, IHasExceptionMessage<TSelf, TInput>, IGuard<TSelf, TInput>, new();

/// <summary>
/// A guard that (in)validates and also outputs a value.
/// </summary>
/// <typeparam name="TInput">The input parameter(s) of the check.</typeparam>
/// <typeparam name="TMessageParam">The parameters needed to construct to external validation message.</typeparam>
/// <typeparam name="TOutput"></typeparam>
public abstract record OutputGuardBase<TSelf, TInput, TMessageParam, TOutput> : IGuard<TSelf>
	where TSelf : OutputGuardBase<TSelf, TInput, TMessageParam, TOutput>, IOutputGuard<TInput, TOutput>, IHasExceptionMessage<TSelf, TMessageParam>, IGuard<TSelf, TMessageParam>, new()
{
	public static TReturn ThrowException<TReturn>(string objectName, TMessageParam messageParameter, object? errorCode, Exception? innerException = null,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null,
		[CallerLineNumber] int? callerLineNumber = null)
		=> IGuard<TSelf, TMessageParam>.ThrowException<TReturn>(objectName, messageParameter, errorCode, innerException, callerMemberName, callerFilePath, callerLineNumber);

	public static TOutput Guard(Validator validator, TInput input, TMessageParam messageParameter, object? errorCode, Exception? innerException = null,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null,
		[CallerLineNumber] int? callerLineNumber = null)
	{
		if (!TSelf.IsValid(input, out var output))
			validator.Throw(IGuard<TSelf>.CreateException(
				message: IGuard<TSelf, TMessageParam>.GetMessage(objectName: validator.ObjectName, messageParameter),
				errorCode,
				innerException,
				callerMemberName, callerFilePath, callerLineNumber));

		return output!;
	}
}
