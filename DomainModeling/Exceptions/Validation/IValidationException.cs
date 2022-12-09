namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// <para>
/// An exception which occurs after invalidation of external input.
/// </para>
/// <para>
/// Validation exceptions contain an <see cref="ErrorCode"/> and <see cref="ExternalMessage"/> which are communicated externally.
/// It helps localization of messages that are shown to the end-user. To consume and localize these messages, see: https://github.com/Code-Chops/DomainDrivenDesign.Contracts.
/// </para>
/// </summary>
public interface IValidationException : ICustomException
{
	string ErrorCode { get; }
	ValidationExceptionMessage ExternalMessage { get; }
}
