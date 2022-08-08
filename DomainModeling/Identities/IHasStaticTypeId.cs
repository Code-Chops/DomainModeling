namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// Contains a static type discriminator. 
/// </summary>
public interface IHasStaticTypeId
{
	public static abstract Id StaticTypeId { get; }
}