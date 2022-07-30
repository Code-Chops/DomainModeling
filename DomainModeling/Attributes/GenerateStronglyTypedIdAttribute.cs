namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// <para>
/// <inheritdoc cref="GenerateStronglyTypedId{TNumber}"/>
/// </para>
/// <para>
/// Uses ulong (default) as integral value of the ID.
/// </para>
/// </summary>
public sealed class GenerateStronglyTypedId : GenerateStronglyTypedId<ulong>
{
}

/// <summary>
/// Generates a strongly typed ID for the object (entity) and assigns the ID.
/// </summary>
/// <typeparam name="TNumber">The integral type that will be used for the ID (ulong, int, etc.).</typeparam>
[AttributeUsage(AttributeTargets.Class)]
public class GenerateStronglyTypedId<TNumber> : Attribute
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
}