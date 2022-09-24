namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Core;

public interface IUserException<out TException, in TParameter> : IDomainObject
	where TException : UserException<TException, TParameter>, IUserException<TException, TParameter>
{
	public static abstract string ErrorMessage { get; }
	
	public static abstract TException Create(IId<string> errorCode, TParameter parameter);
}