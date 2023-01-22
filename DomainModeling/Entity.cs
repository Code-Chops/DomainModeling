namespace CodeChops.DomainModeling;

/// <inheritdoc cref="IEntity"/>
[GenerateIdentity]
public abstract partial class Entity<TId> : IEntity, IEquatable<Entity<TId>?>, IHasId<TId>
	where TId : IId<TId>
{
	public override string ToString() => this.ToDisplayString(new { this.Id });

	protected Entity(TId id)
	{
		this.Id = id;
	}
}
