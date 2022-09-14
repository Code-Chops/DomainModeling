namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public interface ISystemException<out TException, in TParameter> : IDomainObject
	where TException : ISystemException<TException, TParameter>
{
	public static abstract TException Create(TParameter parameter);
	
	public static abstract string ErrorMessage { get; }
}