namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// <para>
/// Should be immutable, lightweight, should have structural equality, and therefore shouldn't contain an ID.
/// Put most of the business logic in these objects. These objects can belong to multiple <see cref="AggregateRoot{TSelf}"/>s.
/// </para>
/// <para>
/// Value objects generally have a zero lifespan; they can be created and destroyed with ease.
/// Therefore, record structs preferably should be used, as they implement structural equality and live on the stack.
/// </para>
/// <para>
/// E.g.: When comparing dollar bills we don't care if it is the same bill as yesterday, we only care about the value of it.
/// Don’t introduce separate tables for value objects, just inline them into the parent entity’s table.
/// </para>
/// </summary>
public interface IValueObject : IDomainObject
{
}