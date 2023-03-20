namespace CodeChops.DomainModeling.Identities;

/// <summary>
/// A singleton ID for entities that only have one ID per type. 
/// </summary>
public sealed record SingletonId<TEntity> : Id<SingletonId<TEntity>, string>
	where TEntity : Entity<SingletonId<TEntity>>
{
	[SetsRequiredMembers]
#pragma warning disable CS8618
    public SingletonId()
#pragma warning restore CS8618
    {
	    this.Value = typeof(TEntity).FullName ?? typeof(TEntity).Name;
    }
}
