using CodeChops.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public abstract class Entity<TId> : IHasId<TId>, IEntity<TId>
	where TId : IId
{
	public sealed override bool Equals(object? obj) => obj is Entity<TId> entity && this.GetId() == entity.GetId();
	public sealed override int GetHashCode() => HashCode.Combine(this.Id);
	public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => EqualityComparer<Entity<TId>>.Default.Equals(left, right);
	public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !(left == right);

	public abstract TId Id { get; }

	public IId GetId() => this.Id;
}

public interface IEntity<out TId> : IEntity
	where TId : IId
{
	TId Id { get; }
}

public interface IEntity : IHasId, IDomainObject
{
}