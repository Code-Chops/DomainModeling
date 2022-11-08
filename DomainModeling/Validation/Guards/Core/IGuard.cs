// ReSharper disable ExplicitCallerInfoArgument

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

public interface IGuard<TSelf, in TMessageParam> : IGuard<TSelf>
	where TSelf : IGuard<TSelf, TMessageParam>, IHasExceptionMessage<TSelf, TMessageParam>
{
	public void Throw<TException>(string objectName, TMessageParam messageParameter, IErrorCode? errorCode, Exception? innerException,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
		where TException : ISystemException
		=> this.Throw<TException, int>(objectName, messageParameter, errorCode, innerException, callerMemberName, callerFilePath, callerLineNumber);

	[DoesNotReturn]
	public TReturn Throw<TException, TReturn>(string objectName, TMessageParam messageParameter, IErrorCode? errorCode, Exception? innerException,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
		where TException : ISystemException
	{
		var message = GetMessage(objectName, messageParameter);
		var exception = CreateException(message, errorCode, innerException, callerMemberName, callerFilePath, callerLineNumber);
		
		return exception.Throw<TReturn>();
	}
	
	public static ExceptionMessage<TSelf> GetMessage<TObject>(TMessageParam messageParameter)
		where TObject : IDomainObject
		=> GetMessage(typeof(TObject).Name, messageParameter);
	
	public static ExceptionMessage<TSelf> GetMessage(string objectName, TMessageParam messageParameter)
	{
		var message = TSelf.GetMessage(objectName, messageParameter);
		
		return messageParameter is ITuple tuple
			? new ExceptionMessage<TSelf>(objectName, message, tuple.GetEnumerable())
			: new ExceptionMessage<TSelf>(objectName, message, messageParameter);
	}
}

public interface IGuard<TSelf> : IGuard
	where TSelf : IGuard<TSelf>
{
	public static CustomException CreateException(ExceptionMessage<TSelf> message, IErrorCode? errorCode, Exception? innerException,
		[CallerMemberName] string? callerMemberName = null,
		[CallerFilePath] string? callerFilePath = null, 
		[CallerLineNumber] int? callerLineNumber = null)
	{
		return errorCode is null
			? new SystemException<TSelf>(message, innerException, callerMemberName, callerFilePath, callerLineNumber)
			: new ValidationException<TSelf>(errorCode, message, innerException);
	}
}

public interface IGuard
{
}
