namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// TODO
/// </summary>
[AttributeUsage(AttributeTargets.Struct)]
public sealed class GenerateStringValueObject : Attribute
{
	/// <param name="name">If not provided, 'Identity' will be the default name.</param>
	/// <param name="propertyName">If not provided, 'Id' will be the default property name.</param>
	// ReSharper disable twice UnusedParameter.Local
	public GenerateStringValueObject(string? name = null, string? propertyName = null)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TIntegral">TODO</typeparam>
[AttributeUsage(AttributeTargets.Struct)]
public sealed class GenerateIntegralValueObject<TIntegral> : Attribute
	where TIntegral : IEquatable<TIntegral>, IComparable<TIntegral>
{
	public GenerateIntegralValueObject(string? name = null, string? propertyName = null)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}