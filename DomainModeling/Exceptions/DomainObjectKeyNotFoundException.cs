namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class DomainObjectKeyNotFoundException<TId, TDomainObject> : Exception, ICustomException
{
	public static string ErrorMessage { get; } = $"{typeof(TId).Name} of {typeof(TDomainObject).Name} not found in {typeof(TDomainObject).Name}.";
	
	public static dynamic Throw(string errorMessage)
		=> new DomainObjectKeyNotFoundException<TId, TDomainObject>(errorMessage);

	public static dynamic Throw(TId id, string? callerName = null) 
		=> Throw($"{ErrorMessage}. {nameof(Id)} = {id}, {nameof(callerName)} = {callerName}");

	private DomainObjectKeyNotFoundException(string errorMessage)
		: base(errorMessage)
	{
	}
}