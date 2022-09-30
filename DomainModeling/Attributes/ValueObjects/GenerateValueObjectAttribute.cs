namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060

/// <summary>
/// Generates a value object with a single structural value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the only structural value.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateValueObjectAttribute<T> : Attribute
	where T : struct, IComparable<T>, IEquatable<T>
{
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="generateDefaultConstructor">Generates a default constructor. Default: true.</param>
	/// <param name="addParameterlessConstructor">
	/// If true, it forces to create a parameterless constructor (in order to define a default value).
	/// If false, it creates an obsolete parameterless private constructor that throws an exception.
	/// Default: false.
	/// </param>
	/// <param name="generateEmptyStatic">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: Dictionary.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable).</param>
	/// <param name="allowNull">Allow the string to be null. Default: false.</param>
	/// <param name="minimumValue">The minimum value. Default: no minimum value.</param>
	/// <param name="maximumValue">The maximum value. Default: no maximum value.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateValueObjectAttribute(
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool generateDefaultConstructor = true,
		bool addParameterlessConstructor = false, 
		bool generateEmptyStatic = false,
		string? propertyName = null,
		bool propertyIsPublic = false,
		bool allowNull = false,
		int minimumValue = Int32.MinValue, 
		int maximumValue = Int32.MinValue)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

#pragma warning restore IDE0060