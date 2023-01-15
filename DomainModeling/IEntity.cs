namespace CodeChops.DomainModeling;

/// <inheritdoc cref="IEntity"/>
public interface IEntity<TSelf> : IEntity, IEquatable<TSelf?>
	where TSelf : IEntity<TSelf>
{
}

/// <summary>
/// <para>
/// Entities have an inherent identity and therefore need an ID.
/// In order to generate an ID, add one of the following attribute to your entity:
/// <list type="bullet">
/// <item><see cref="GenerateIdentity"/></item>
/// <item><see cref="GenerateIdentity{T}"/></item>
/// </list>
/// Entities have a long lifespan and are mutable.
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
public interface IEntity : IDomainObject, IHasId
{
}
