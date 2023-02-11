namespace CodeChops.DomainModeling;

/// <summary>
/// <para>
/// An aggregate simplifies the domain model by clustering multiple entities under a single abstraction, representing a transactional boundary.
/// </para>
/// <para>
/// Aggregates have a set of invariants which it maintains during its lifetime. They assure that an aggregate will be in a valid state at all time.
/// </para>
/// <para>
/// An aggregate root faces outwards and controls all access to the objects insides the boundary.
/// It’s the only object that other objects can interact with and should make sense by its own.
/// </para>
/// <para>
/// Most aggregates consist of 1 or 2 entities. 3 entities per aggregate is usually a maximum.
/// Internal entities should not be exposed outside the aggregate.
/// </para>
/// <para>
/// Aggregate roots are being used by <see cref="IApplicationService"/>s, repositories, public APIs, etc.
/// Aggregates can only reference to each other by id.
/// </para>
/// </summary>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot 
	where TId : IId<TId>
{
	public override string ToString() => this.ToDisplayString(extraText: nameof(AggregateRoot<TId>));
}
