using CodeChops.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainModeling.Exceptions.Validation;

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
	internal ValidationException(object errorCode, ValidationExceptionMessage validationMessage, Exception? innerException = null)
		: base(errorCode, validationMessage, innerException)
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
	public string ErrorCode { get; }
	
	public ValidationExceptionMessage ExternalMessage { get; }
	
	/// <param name="errorCode">Is communicated externally!</param>
	/// <param name="validationMessage">Is communicated externally!</param>
	internal ValidationException(object errorCode, ValidationExceptionMessage validationMessage, Exception? innerException = null)
		: base(message: $"{validationMessage} (error code: {errorCode}).", innerException)
	{
		this.ErrorCode = errorCode.ToString() ?? throw new ArgumentNullException(nameof(errorCode));
		this.ExternalMessage = validationMessage;
	}
}

