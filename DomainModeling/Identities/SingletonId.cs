namespace CodeChops.DomainModeling.Identities;

/// <summary>
/// A singleton ID for entities that only have one ID per type. 
/// </summary>
public sealed record SingletonId<TEntity> : Id<SingletonId<TEntity>, string>
	where TEntity : Entity<SingletonId<TEntity>>
{
	[SetsRequiredMembers]
    public SingletonId()
    {
	    this.Value = typeof(TEntity).FullName ?? typeof(TEntity).Name;
    }
}
