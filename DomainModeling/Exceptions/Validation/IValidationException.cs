namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// An exception which occurs after validation of external input.
/// </summary>
public interface IValidationException : ICustomException
{
	IErrorCode ErrorCode { get; }
}
