namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// <para>
/// <b>Be sure that structural equality comparison is implemented for all children! Also implement ToString().</b>
/// </para>
/// <para>
/// A value object should be immutable, lightweight, should have structural equality, and therefore shouldn't contain an ID.
/// E.g.: When comparing dollar bills we don't care if it is the same bill as yesterday, we only care about the value of it.
/// </para>
/// <para>
/// Value objects generally have a zero lifespan; they can be created and destroyed with ease.
/// Therefore, record structs preferably should be used, as they implement structural equality and live on the stack.
/// </para>
/// <para>
/// Don’t introduce separate tables for value objects, just inline them into the parent entity’s table.
/// </para>
/// <para>
/// Value objects can belong to multiple <see cref="AggregateRoot"/>s.
/// </para>
/// </summary>
public interface IValueObject : IDomainObject
{
}