namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class DomainObjectKeyNotFoundException<TId, TDomainObject> : SystemException<DomainObjectKeyNotFoundException<TId, TDomainObject>>, ISystemException<DomainObjectKeyNotFoundException<TId, TDomainObject>>
{
	public static string ErrorMessage { get; } = $"{typeof(TId).Name} of {typeof(TDomainObject).Name} not found in {typeof(TDomainObject).Name}.";

	public static DomainObjectKeyNotFoundException<TId, TDomainObject> Create(object parameters)
		=> new(parameters);

	public DomainObjectKeyNotFoundException(object parameters) : base(parameters)
	{
	}
}