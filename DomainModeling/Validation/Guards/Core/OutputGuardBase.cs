// ReSharper disable ExplicitCallerInfoArgument

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

public abstract record OutputGuardBase<TSelf, TInput, TOutput> : OutputGuardBase<TSelf, TInput, TOutput, TInput>
	where TSelf : OutputGuardBase<TSelf, TInput, TOutput>, IOutputGuard<TInput, TOutput>, IHasExceptionMessage<TSelf, TInput>, IGuard<TSelf, TInput>, new();

public abstract record OutputGuardBase<TSelf, TInput, TOutput, TMessageParam> : IGuard<TSelf>
	where TSelf : OutputGuardBase<TSelf, TInput, TOutput, TMessageParam>, IOutputGuard<TInput, TOutput>, IHasExceptionMessage<TSelf, TMessageParam>, IGuard<TSelf, TMessageParam>, new()
{
	public static TOutput Guard(Validator validator, TInput input, TMessageParam messageParameter, IErrorCode? errorCode, Exception? innerException, 
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
	{
		if (!TSelf.IsValid(input, out var output))
			validator.Throw(IGuard<TSelf>.CreateException(IGuard<TSelf, TMessageParam>.GetMessage(objectName: validator.ObjectName, messageParameter), errorCode, innerException, callerMemberName, callerFilePath, callerLineNumber));

		return output;
	}
}
