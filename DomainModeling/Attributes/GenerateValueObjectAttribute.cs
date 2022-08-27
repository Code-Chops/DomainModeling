namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// Generates a value object with a single structural value of type <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">The type of the only structural value.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateValueObjectAttribute<TValue> : Attribute
	where TValue : struct, IComparable<TValue>, IEquatable<TValue>, IConvertible
{
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="generateDefaultConstructor">Generates a default constructor. Default: true.</param>
	/// <param name="generateParameterlessConstructor">
	/// Generates a parameterless constructor that assigns a default value to the property.
	/// If false, it creates an obsolete parameterless private constructor that throws an exception.
	/// Default: false.
	/// </param>
	/// <param name="generateEmptyStatic">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: Dictionary.</param>
	/// <param name="minimumValue">The minimum value. Default: no minimum value.</param>
	/// <param name="maximumValue">The maximum value. Default: no maximum value.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateValueObjectAttribute(
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool generateDefaultConstructor = true,
		bool generateParameterlessConstructor = false, 
		bool generateEmptyStatic = false,
		string? propertyName = null,
		int minimumValue = Int32.MinValue, 
		int maximumValue = Int32.MinValue)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}