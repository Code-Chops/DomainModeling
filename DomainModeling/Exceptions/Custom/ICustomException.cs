namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public interface ICustomException<out TException> : IDomainObject
	where TException : ICustomException<TException>
{
	public static abstract TException Create(string message);
	
	public static abstract string ErrorMessage { get; }
}