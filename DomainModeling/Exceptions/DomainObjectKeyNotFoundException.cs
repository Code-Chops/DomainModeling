using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class DomainObjectKeyNotFoundException<TId, TDomainObject> : KeyNotFoundException, ICustomException<DomainObjectKeyNotFoundException<TId, TDomainObject>>
{
	public static string ErrorMessage { get; } = $"{typeof(TId).Name} of {typeof(TDomainObject).Name} not found in {typeof(TDomainObject).Name}.";
	
	public static DomainObjectKeyNotFoundException<TId, TDomainObject> Create(TId id, string? callerName = null) 
		=> new ($"{ErrorMessage}. {nameof(Id)} = {id}, {nameof(callerName)} = {callerName}");

	public static DomainObjectKeyNotFoundException<TId, TDomainObject> Create(string errorMessage) => new(errorMessage);
	private DomainObjectKeyNotFoundException(string errorMessage) : base(errorMessage) { }
}