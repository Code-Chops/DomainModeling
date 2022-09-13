namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public interface ISystemException<out TException> : IDomainObject
	where TException : ISystemException<TException>
{
	public static abstract TException Create(object parameters);
	
	public static abstract string ErrorMessage { get; }
}

public interface ISystemException<out TException, in TParameter> : IDomainObject
	where TException : ISystemException<TException, TParameter>
{
	public static abstract TException Create(TParameter parameter);
	
	public static abstract string ErrorMessage { get; }
}