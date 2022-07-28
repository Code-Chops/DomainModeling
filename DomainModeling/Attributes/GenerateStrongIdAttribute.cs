namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// Uses ULong as integral value of the ID.
/// </summary>
public sealed class GenerateEntityId : GenerateEntityId<uint>
{
}

[AttributeUsage(AttributeTargets.Class)]
public class GenerateEntityId<TNumber> : Attribute
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
}