namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// Generates a value object with a single structural value of type <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">The type of the only structural value.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateValueObjectAttribute<TValue> : Attribute
	where TValue : struct, IComparable<TValue>, IEquatable<TValue>, IConvertible
{
	/// <param name="minimumValue">The minimum value. Default: no minimum value.</param>
	/// <param name="maximumValue">The maximum value. Default: no maximum value.</param>
	// ReSharper disable twice UnusedParameter.Local
	public GenerateValueObjectAttribute(int minimumValue = Int32.MinValue, int maximumValue = Int32.MinValue)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}