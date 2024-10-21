namespace CodeChops.DomainModeling;

/// <inheritdoc cref="IEntity"/>
public abstract class Entity<TId>(TId id) : IEntity, IEquatable<Entity<TId>?>, IHasId<TId>
	where TId : IId, IHasDefault<TId>
{
	public override string ToString() => this.ToDisplayString(new { this.Id });

	public TId Id { get; } = id;

	public IId GetId() => this.Id;

	public sealed override int GetHashCode()
	{
		return this.Id.HasDefaultValue
			? RuntimeHelpers.GetHashCode(this)
			: this.Id.GetHashCode();
	}

	public bool Equals(Entity<TId>? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		if (other.GetType() != this.GetType()) return false;

		return !this.Id.HasDefaultValue && this.Id.Equals(other.Id);
	}

	public sealed override bool Equals(object? obj)
	{
		return obj is Entity<TId> other
		       && obj.GetType() == this.GetType()
		       && this.Equals(other);
	}

	public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
	{
		if (left is null && right is null) return true;
		if (left is null || right is null) return false;
		return left.Equals(right);
	}

	public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
		=> !(left == right);
}
