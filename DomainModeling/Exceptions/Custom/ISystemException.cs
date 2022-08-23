namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public interface ISystemException<out TException> : IDomainObject
	where TException : ISystemException<TException>
{
	public static abstract TException Create(object parameters);
	
	public static abstract string ErrorMessage { get; }
}