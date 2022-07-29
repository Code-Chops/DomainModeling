namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// Contains a static type discriminator. 
/// </summary>
public interface IHasStaticTypeId<out TTypeId>
	where TTypeId : Id
{
	public static abstract TTypeId StaticTypeId { get; }
}

/// <summary>
/// Used to get the static type ID of runtime types (when the type is not known).
/// </summary>
public interface IHasStaticTypeId
{
	Id GetStaticTypeId();
}