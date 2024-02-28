namespace CodeChops.DomainModeling;

/// <summary>
/// <para>
/// Value objects should be immutable, lightweight, have structural equality, and therefore shouldn't contain an ID.
/// E.g.: When comparing dollar bills we don't care if it is the same bill as yesterday, we only care about the value of it.
/// </para>
/// <para>
/// Implement a value object using one of these methods (ordered by preference):
/// <list type="number">
/// <item>
/// 	<para>Use the <b>value object generator</b>.</para>
/// 	By first creating a (ordered by preference):
/// 	<list type="bullet">
/// 	<item>A <em>readonly ref partial struct</em>:<br/>
/// 	These types live on the stack. This is preferable as value objects generally can be created and destroyed with ease.
/// 	</item>
/// 	<item>A <em>readonly partial record struct</em>:<br/>
/// 	Value objects should be immutable (readonly).
/// 	</item>
/// 	<item>A <em>partial record</em>:<br/>
/// 	Records have built-in structural equality and therefore it's a good convention to always them for value objects.</item>
/// 	</list>
///		And use one of the following attributes, depending on the value type to be wrapped:
///		<list type="bullet">
/// 	<item>Struct: <see cref="GenerateValueObjectAttribute{T}"/></item>
/// 	<item>String: <see cref="GenerateStringValueObjectAttribute"/></item>
/// 	<item>List: <see cref="GenerateListValueObjectAttribute{T}"/></item>
/// 	<item>Dictionary: <see cref="GenerateDictionaryValueObjectAttribute{TKey, TValue}"/></item>
/// 	</list>
/// </item>
/// <item>
///		Implement the abstract record <see cref="ValueObject{TSelf}"/>.
/// </item>
/// <item>
///		Implement the abstract class <see cref="ValueObjectClass{TSelf}"/>.
/// </item>
/// </list>
/// </para>
/// <para>
/// Don’t introduce separate tables for value objects in the database. Just inline them into the parent entity’s table.
/// </para>
/// <para>
/// Value objects can belong to multiple <see cref="IAggregateRoot"/>s.
/// </para>
/// </summary>
public interface IValueObject : IDomainObject;
