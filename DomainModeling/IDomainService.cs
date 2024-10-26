namespace CodeChops.DomainModeling;

/// <summary>
/// <para>
/// Contains domain logic. Possesses knowledge that doesn't belong to individual entities and value objects.
/// </para>
/// <para>
/// Must be stateless and reside in the domain layer. Shouldn't communicate with classes outside the domain layer.
/// </para>
/// </summary>
public interface IDomainService;
