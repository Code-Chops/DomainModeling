namespace CodeChops.DomainModeling.Attributes;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// <inheritdoc cref="GenerateIdentity{TUnderlying}"/>
/// <para>
/// The underlying type of the ID is ulong.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateIdentity : Attribute
{
	/// <param name="name">If not provided, 'Identity' will be the default name.</param>
	/// <param name="propertyName">If not provided, 'Id' will be the default property name.</param>
	// ReSharper disable twice UnusedParameter.Local
	public GenerateIdentity(string? name = null, string? propertyName = null)
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
/// <typeparam name="T">The underlying value of the ID.</typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateIdentity<T> : Attribute
	where T : IEquatable<T>, IComparable<T>
{
	/// <param name="name">If not provided, 'Identity' will be the default name.</param>
	/// <param name="propertyName">If not provided, 'Id' will be the default property name.</param>
	public GenerateIdentity(string? name = null, string? propertyName = null)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

#pragma warning restore IDE0060 // Remove unused parameter
