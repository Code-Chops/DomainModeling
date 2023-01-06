namespace CodeChops.DomainModeling;

/// <summary>
/// <para>
/// Entities have an inherent identity and therefore need an ID. Entities have a long lifespan and are mutable.
/// They can belong to only one single <see cref="AggregateRoot"/>.
/// </para>
/// <para>
/// If an entity uses a collection under the hood, you can make use of the following base-entities:
/// <list type="bullet">
/// <item><see cref="DictionaryEntity{TSelf,TKey,TValue}"/></item>
/// <item><see cref="HashSetEntity{T}"/></item>
/// <item><see cref="ListEntity{TSelf, TElement}"/></item>
/// </list>
/// </para>
/// </summary>
[GenerateIdentity]
public abstract partial class Entity : IDomainObject, IEquatable<Entity?>, IHasId
{
	public override string ToString() => this.ToDisplayString(new { this.Id });

}
