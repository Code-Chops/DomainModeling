namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// Contains a static type discriminator. 
/// </summary>
public interface IHasStaticTypeId<out TTypeId>
	where TTypeId : IId
{
	public static abstract TTypeId StaticTypeId { get; }
}