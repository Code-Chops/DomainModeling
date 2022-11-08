namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// <para>
/// <em>Be sure that structural equality comparison is implemented for all children! Also implement ToString().</em>
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
/// <para>
/// <em>
/// Use the following attributes to generate simple value objects:
/// <list type="bullet">
/// <item>A value object that wraps a primitive type: <see cref="GenerateValueObjectAttribute{T}"/></item>
/// <item>A value object that wraps a string: <see cref="GenerateStringValueObjectAttribute"/></item>
/// <item>A value object that wraps a list: <see cref="GenerateListValueObjectAttribute{T}"/></item>
/// <item>A value object that wraps a dictionary: <see cref="GenerateDictionaryValueObjectAttribute{TKey, TValue}"/></item>
/// </list>
/// </em>
/// </para>
/// </summary>
public interface IValueObject : IDomainObject
{
}
