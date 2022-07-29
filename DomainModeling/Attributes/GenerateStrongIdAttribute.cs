namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// Generates a custom ID type for the entity and assigns it to the entity. Uses ULong as integral value of the ID.
/// </summary>
public sealed class GenerateEntityId : GenerateEntityId<uint>
{
}

/// <summary>
/// Generates a custom ID type for the entity and assigns it to the entity.
/// </summary>
/// <typeparam name="TNumber">The integral type that will be used for the ID (ulong, int, etc.).</typeparam>
[AttributeUsage(AttributeTargets.Class)]
public class GenerateEntityId<TNumber> : Attribute
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
}