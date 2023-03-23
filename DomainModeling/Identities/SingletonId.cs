namespace CodeChops.DomainModeling.Identities;

/// <summary>
/// A singleton ID for entities that only have one ID per type. 
/// </summary>
public record SingletonId<TEntity> : IId<SingletonId<TEntity>, string>
	where TEntity : IId<TEntity>
{
	public int CompareTo(SingletonId<TEntity>? other) => String.Compare(this.Value, other?.Value ?? "", StringComparison.Ordinal);

	public override string ToString() => $"{typeof(SingletonId<TEntity>).Name} of {typeof(TEntity).Name}";
	
	public static SingletonId<TEntity> Default { get; } = new();
	public string Value { get; } = typeof(TEntity).FullName!;

	public bool HasDefaultValue { get; } = true;

	private SingletonId()
	{
	}
}
