namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// <inheritdoc cref="GenerateStronglyTypedId{TPrimitive}"/>
/// <para>
/// The primitive type of the ID is ulong.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateStronglyTypedId : Attribute
{
	/// <param name="name">If not provided, 'Identity' will be the default name.</param>
	/// <param name="propertyName">If not provided, 'Id' will be the default property name.</param>
	// ReSharper disable twice UnusedParameter.Local
	public GenerateStronglyTypedId(string? name = null, string? propertyName = null)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
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
	where TPrimitive : struct, IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	/// <param name="name">If not provided, 'Identity' will be the default name.</param>
	/// <param name="propertyName">If not provided, 'Id' will be the default property name.</param>
	public GenerateStronglyTypedId(string? name = null, string? propertyName = null)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

#pragma warning restore IDE0060 // Remove unused parameter
