namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User.Core;

/// <summary>
/// An exception which should be communicated to the user.
/// </summary>
public interface IUserException
{
	string? ErrorCode { get; }
}
