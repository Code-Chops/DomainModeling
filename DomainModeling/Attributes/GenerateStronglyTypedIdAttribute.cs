namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// <inheritdoc cref="GenerateStronglyTypedId{TNumber}"/>
/// <para>
/// The primitive type of the ID is ulong (default).
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateStronglyTypedId : Attribute
{
	public GenerateStronglyTypedId(string? name = null)
	{
		// These parameters will be read from the attribute arguments itself.
	}
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
/// <typeparam name="TPrimitive">The primitive value of the ID.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateStronglyTypedId<TPrimitive> : Attribute
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	/// <param name="baseType">If not provided, the base type will be an ID with ulong as primitive value.</param>
	/// <param name="name">If not provided, 'Identity' will be the default name.</param>
	public GenerateStronglyTypedId(Type? baseType = null, string? name = null)
	{
		// These parameters will be read from the attribute arguments itself.
	}
}