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
	/// <param name="validationMessage">Is communicated externally!</param>
	// ReSharper disable twice ExplicitCallerInfoArgument
	internal ValidationException(IErrorCode errorCode, ValidationExceptionMessage validationMessage, Exception? innerException = null)
		: base(errorCode, validationMessage, innerException)
	{
	}
}

/// <summary>
/// <inheritdoc cref="IValidationException"/>
/// </summary>
// ReSharper disable once UnusedTypeParameter
public class ValidationException : CustomException, IDomainObject, IValidationException
{
	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public IErrorCode ErrorCode { get; }
	
	public ValidationExceptionMessage ValidationValidationMessage { get; }
	
	/// <param name="errorCode">Is communicated externally!</param>
	/// <param name="validationMessage">Is communicated externally!</param>
	internal ValidationException(IErrorCode errorCode, ValidationExceptionMessage validationMessage, Exception? innerException = null)
		: base(message: $"{validationMessage} (error code: {errorCode}).", innerException)
	{
		this.ErrorCode = errorCode;
		this.ValidationValidationMessage = validationMessage;
	}
}

