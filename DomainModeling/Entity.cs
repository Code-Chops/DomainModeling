using CodeChops.DomainDrivenDesign.DomainModeling.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// <para>
/// Entities have an inherent identity and therefore need an ID. Entities have a long lifespan and are mutable.
/// They can belong to only one single <see cref="AggregateRoot{TSelf}"/>.
/// </para>
/// </summary>
public abstract class Entity : IHasId, IDomainObject, IEquatable<Entity?>
{
	public override string ToString() => $"{{{this.GetType().Name} Id={this.Id}}}";

	public abstract Id Id { get; }
	
	public sealed override int GetHashCode()
	{
		return this.Id.HasDefaultValue
			? HashCode.Combine(this)
			: this.Id.GetHashCode();
	}
	
	public sealed override bool Equals(object? obj)
	{
		return obj is Entity other 
		       && obj.GetType() == this.GetType() 
		       && this.Equals(other);
	}

	public virtual bool Equals(Entity? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		if (other.GetType() != this.GetType()) return false;
		
		return !this.Id.HasDefaultValue && this.Id.Equals(other.Id);
	}
}