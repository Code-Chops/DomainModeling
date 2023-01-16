// ReSharper disable ExplicitCallerInfoArgument

namespace CodeChops.DomainModeling.Validation.Guards.Core;

/// <summary>
/// <inheritdoc cref="NoOutputGuardBase{TSelf,TInput,TMessageParam}"/>
/// </summary>
public abstract record NoOutputGuardBase<TSelf, TInput> : NoOutputGuardBase<TSelf, TInput, TInput> 
	where TSelf : NoOutputGuardBase<TSelf, TInput, TInput>, INoOutputGuard<TInput>, IHasExceptionMessage<TSelf, TInput>, IGuard<TSelf, TInput>;

/// <summary>
/// A guard that does not output a value, but only (in)validates.
/// </summary>
/// <typeparam name="TInput">The input parameter(s) of the check.</typeparam>
/// <typeparam name="TMessageParam">The parameters needed to construct to external validation message.</typeparam>
public abstract record NoOutputGuardBase<TSelf, TInput, TMessageParam> : IGuard<TSelf>
	where TSelf : NoOutputGuardBase<TSelf, TInput, TMessageParam>, INoOutputGuard<TInput>, IHasExceptionMessage<TSelf, TMessageParam>, IGuard<TSelf, TMessageParam>
{
	public static Validator Guard(Validator validator, TInput input, TMessageParam messageParameter, object? errorCode, Exception? innerException,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
	{
		if (!TSelf.IsValid(input))
			validator.Throw(IGuard<TSelf>.CreateException(
				message: IGuard<TSelf, TMessageParam>.GetMessage(validator.ObjectName, messageParameter), 
				errorCode, 
				innerException,
				callerMemberName, callerFilePath, callerLineNumber));

		return validator;
	}
}
