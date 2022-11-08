using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// <inheritdoc cref="IValidationException"/>
/// </summary>
// ReSharper disable once UnusedTypeParameter
public class ValidationException<TGuard> : ValidationException
	where TGuard : IGuard
{
	/// <param name="errorCode">Is communicated externally!</param>
	/// <param name="message">Is communicated externally!</param>
	// ReSharper disable twice ExplicitCallerInfoArgument
	internal ValidationException(IErrorCode errorCode, ExceptionMessage<TGuard> message, Exception? innerException = null)
		: base(errorCode, message: $"{message} (error code: {errorCode}).", innerException)
	{
	}
}

/// <summary>
/// <inheritdoc cref="IValidationException"/>
/// </summary>
// ReSharper disable once UnusedTypeParameter
public class ValidationException : CustomException, IValidationException
{
	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public IErrorCode ErrorCode { get; }
	
	/// <param name="errorCode">Is communicated externally!</param>
	/// <param name="message">Is communicated externally!</param>
	public ValidationException(IErrorCode errorCode, string message, Exception? innerException = null)
		: base(message: $"{message} (error code: {errorCode}).", innerException)
	{
		this.ErrorCode = errorCode;
	}
}

