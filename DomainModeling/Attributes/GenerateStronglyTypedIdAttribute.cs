namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// <para>
/// <inheritdoc cref="GenerateStronglyTypedId{TNumber}"/>
/// </para>
/// Uses ulong (default) as integral value of the ID.
/// </summary>
public sealed class GenerateStronglyTypedId : GenerateStronglyTypedId<ulong>
{
}

/// <summary>
/// <para>
/// Generates a strongly typed ID for the object (entity) and assigns the ID.
/// </para>
/// <para>
/// Also generates comparison and equality based on the ID value.<br/>
/// If the ID has a default value, comparison is done by their reference.
/// </para>
/// </summary>
/// <typeparam name="TNumber">The integral type that will be used for the ID (ulong, int, etc.).</typeparam>
[AttributeUsage(AttributeTargets.Class)]
public class GenerateStronglyTypedId<TNumber> : Attribute
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
}