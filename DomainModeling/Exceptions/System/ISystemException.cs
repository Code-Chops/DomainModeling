namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;

/// <summary>
/// An exception that contains system-information and therefore should not be visible to the user.
/// </summary>
public interface ISystemException : ICustomException
{
}
