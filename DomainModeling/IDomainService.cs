namespace CodeChops.DomainModeling;

/// <summary>
/// <para>
/// Contains domain logic. Possesses knowledge that doesn't belong to individual entities and value objects.
/// </para>
/// <para>
/// Must must be stateless and reside in the domain layer. Shouldn't communicate with classes outside of the domain layer.
/// </para>
/// </summary>
public interface IDomainService : IDomainObject;
