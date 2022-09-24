namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Core;

public interface ISystemException<out TException, in TParameter> : IDomainObject
	where TException : ISystemException<TException, TParameter>
{
	public static abstract string ErrorMessage { get; }
	
	public static abstract TException Create(TParameter parameter);
}