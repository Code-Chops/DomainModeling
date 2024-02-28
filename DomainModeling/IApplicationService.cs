namespace CodeChops.DomainModeling;

/// <summary>
/// <para>
/// Communicates with the outside world. Delegates the execution to domain classes like <see cref="IDomainObject"/>s, repositories, APIs, etc.
/// </para>
/// <para>
/// Must be stateless and reside outside of the domain layer. Shouldn't contain domain logic.
/// </para>
/// </summary>
public interface IApplicationService;
