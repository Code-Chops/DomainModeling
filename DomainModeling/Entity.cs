using CodeChops.DomainDrivenDesign.DomainModeling.Attributes;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// <para>
/// Entities have an inherent identity and therefore need an ID. Entities have a long lifespan and are mutable.
/// They can belong to only one single <see cref="AggregateRoot"/>.
/// </para>
/// </summary>
[GenerateStronglyTypedId]
public abstract partial class Entity : IHasId, IDomainObject, IEquatable<Entity?>
{
	public override string ToString() => $"{this.GetType().Name} {{ Id={this.Id} }}";
}