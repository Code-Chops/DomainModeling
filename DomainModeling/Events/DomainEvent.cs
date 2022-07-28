namespace CodeChops.DomainDrivenDesign.DomainModeling.Events;

/// <summary>
/// An event that is significant for your domain model. Should include as little data as possible.
/// Used for:
/// <list type="bullet">
/// <item>Decoupling bounded contexts.</item>
/// <item>Facilitating communication between bounded contexts.</item>
/// <item>Decoupling entities within a single bounded context.</item>
/// </list>
/// <inheritdoc cref="IEvent"/>
/// </summary>
public record DomainEvent : IEvent;	