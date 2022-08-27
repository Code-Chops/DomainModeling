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
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: false.</param>
	/// <param name="prohibitParameterlessConstruction">Creates an obsolete parameterless private constructor that throws an exception. Structs can still be instantiated by using default(). Default: true.</param>
	/// <param name="generateEmptyStatic">Generate a static property with a default value. Default: true.</param>
	/// <param name="propertyName">The name of the property and the backing field. Default: Dictionary (_dictionary).</param>
	/// <param name="minimumValue">The minimum value. Default: no minimum value.</param>
	/// <param name="maximumValue">The maximum value. Default: no maximum value.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateValueObjectAttribute(
		bool generateToString = true,
		bool addCustomValidation = false,
		bool prohibitParameterlessConstruction = true, 
		bool generateEmptyStatic = true,
		string? propertyName = null,
		int minimumValue = Int32.MinValue, 
		int maximumValue = Int32.MinValue)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}