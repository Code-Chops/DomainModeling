// ReSharper disable ExplicitCallerInfoArgument

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

public abstract record NoOutputGuardBase<TSelf, TInput> : NoOutputGuardBase<TSelf, TInput, TInput> 
	where TSelf : NoOutputGuardBase<TSelf, TInput, TInput>, INoOutputGuard<TInput>, IHasExceptionMessage<TSelf, TInput>, IGuard<TSelf, TInput>;

public abstract record NoOutputGuardBase<TSelf, TInput, TMessageParam> : IGuard<TSelf>
	where TSelf : NoOutputGuardBase<TSelf, TInput, TMessageParam>, INoOutputGuard<TInput>, IHasExceptionMessage<TSelf, TMessageParam>, IGuard<TSelf, TMessageParam>
{
	public static Validator Guard(Validator validator, TInput input, TMessageParam messageParameter, IErrorCode? errorCode, Exception? innerException,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
	{
		if (!TSelf.IsValid(input))
			validator.Throw(IGuard<TSelf>.CreateException(IGuard<TSelf, TMessageParam>.GetMessage(validator.ObjectName, messageParameter), errorCode, innerException, callerMemberName, callerFilePath, callerLineNumber));

		return validator;
	}
}
