namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class DomainObjectKeyNotFoundException<TId, TDomainObject> : CustomException<DomainObjectKeyNotFoundException<TId, TDomainObject>>, ICustomException<DomainObjectKeyNotFoundException<TId, TDomainObject>>
{
	public static string ErrorMessage { get; } = $"{typeof(TId).Name} of {typeof(TDomainObject).Name} not found in {typeof(TDomainObject).Name}.";
	
	public static DomainObjectKeyNotFoundException<TId, TDomainObject> Create(TId id, string? callerName = null) 
		=> new ($"{ErrorMessage}. {nameof(Id)} = {id}, {nameof(callerName)} = {callerName}");

	public static DomainObjectKeyNotFoundException<TId, TDomainObject> Create(string? parameters = null) => new(parameters);
	private DomainObjectKeyNotFoundException(string? parameters) : base(parameters) { }
}