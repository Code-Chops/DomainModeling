namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public interface IUserException<out TException> : IDomainObject
	where TException : UserException<TException>, IUserException<TException>
{
	public static abstract string ErrorMessage { get; }
	
	public static abstract TException Create(Id<string> errorCode, string parameterName);
}