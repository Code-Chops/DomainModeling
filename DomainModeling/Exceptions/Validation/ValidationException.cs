using CodeChops.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainModeling.Exceptions.Validation;

/// <inheritdoc cref="ValidationException"/>
// ReSharper disable once UnusedTypeParameter
public class ValidationException<TGuard> : ValidationException
	where TGuard : IGuard
{
	internal ValidationException(object errorCode, ValidationExceptionMessage validationMessage, Exception? innerException = null) 
		: base(errorCode, validationMessage, innerException)
	{
	}
}

/// <summary>
/// An exception which occurs after invalidation of external user input.
/// Validation exceptions contain an <see cref="ErrorCode"/> and <see cref="ExternalMessage"/> which are communicated externally.
/// It helps localization of messages that are shown to the end-user. To consume and localize these messages, see: https://github.com/Code-Chops/DomainDrivenDesign.Contracts.
/// </summary>
public class ValidationException : CustomException
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
	
	/// <summary>
	/// Throws the exception. Returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
	[DoesNotReturn]
	public override TReturn Throw<TReturn>() => throw this;

	/// <summary>
	/// Throws the exception.
	/// </summary>
	[DoesNotReturn]
	public override void Throw() => throw this;
}
