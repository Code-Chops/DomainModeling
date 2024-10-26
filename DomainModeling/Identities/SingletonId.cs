namespace CodeChops.DomainModeling.Identities;

/// <summary>
/// A singleton ID for entities that only have one instance.
/// </summary>
public record SingletonId<TEntity> : IId<SingletonId<TEntity>, SingletonId<TEntity>>,
	ICreatable<SingletonId<TEntity>, SingletonId<TEntity>>
{
	public int CompareTo(SingletonId<TEntity>? other) => 0;

	public override string ToString() => $"{typeof(SingletonId<TEntity>).Name} of {typeof(TEntity).Name}";

	public static SingletonId<TEntity> Default { get; } = new();
	public object GetValue() => this.Value;
	public SingletonId<TEntity> Value => this;

	public bool HasDefaultValue => true;

	private SingletonId()
	{
	}

	public static SingletonId<TEntity> Create(SingletonId<TEntity> parameter, Validator? validator = null)
	{
		return Default;
	}
}
