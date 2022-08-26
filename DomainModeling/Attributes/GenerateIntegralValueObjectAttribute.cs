namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// Generates a value object with a single structural value of primitive type <typeparamref name="TIntegral"/>.
/// </summary>
/// <typeparam name="TIntegral">The integral type of the only structural value.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateIntegralValueObject<TIntegral> : Attribute
	where TIntegral : struct, IComparable<TIntegral>, IEquatable<TIntegral>, IConvertible
{
	/// <param name="minimumValue">The minimum value of the integral. Default: no minimum value.</param>
	/// <param name="maximumValue">The maximum value of the integral. Default: no maximum value.</param>
	// ReSharper disable twice UnusedParameter.Local
	public GenerateIntegralValueObject(int minimumValue = Int32.MinValue, int maximumValue = Int32.MinValue)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}