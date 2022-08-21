namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// Entities have an inherent identity and therefore need an ID. Entities have a long lifespan and are mutable.
/// They can belong to only one single <see cref="AggregateRoot"/>.
/// </summary>
[GenerateStronglyTypedId]
public abstract partial class Entity : IDomainObject, IEquatable<Entity?>, IHasId
{
	public override string ToString() => this.ToEasyString(new { this.Id });
}