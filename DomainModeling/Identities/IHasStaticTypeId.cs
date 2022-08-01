namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// Contains a static type discriminator. 
/// </summary>
public interface IHasStaticTypeId<out TTypeId>
	where TTypeId : Id
{
	public static abstract TTypeId StaticTypeId { get; }
}