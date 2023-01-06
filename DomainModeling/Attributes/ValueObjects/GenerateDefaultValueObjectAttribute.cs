namespace CodeChops.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// <para>Generates a value object with a single structural underlying value of type <typeparamref name="TUnderlyingValue"/>.</para>
/// <inheritdoc cref="GenerateValueObjectAttributeBase"/>
/// </summary>
/// <typeparam name="TUnderlyingValue">The underlying structural value type.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateValueObjectAttribute<TUnderlyingValue> : GenerateValueObjectAttributeBase
	where TUnderlyingValue : IEquatable<TUnderlyingValue>
{
	/// <inheritdoc />
	public GenerateValueObjectAttribute(int minimumValue = Int32.MinValue, int maximumValue = Int32.MaxValue, bool generateToString = true, 
		bool generateComparison = true, bool generateDefaultConstructor = true, bool forbidParameterlessConstruction = true, bool generateStaticDefault = false, 
		string? propertyName = null, bool propertyIsPublic = false, bool valueIsNullable = false, bool useValidationExceptions = true) 
		: base(underlyingType: typeof(TUnderlyingValue), minimumValue, maximumValue, generateToString, generateComparison, generateDefaultConstructor, forbidParameterlessConstruction, 
			generateStaticDefault, propertyName, propertyIsPublic, valueIsNullable, useValidationExceptions)
	{
	}
}

/// <summary>
/// <para>Generates a value object with a single structural underlying value of the provided type.</para>
/// <inheritdoc cref="GenerateValueObjectAttributeBase"/>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateValueObjectAttribute : GenerateValueObjectAttributeBase
{
	/// <inheritdoc />
	public GenerateValueObjectAttribute(Type? underlyingType, int minimumValue = Int32.MinValue, int maximumValue = Int32.MaxValue, bool generateToString = true, 
		bool generateComparison = true, bool generateDefaultConstructor = true, bool forbidParameterlessConstruction = true, bool generateStaticDefault = false, 
		string? propertyName = null, bool propertyIsPublic = false, bool valueIsNullable = false, bool useValidationExceptions = true) 
		: base(underlyingType, minimumValue, maximumValue, generateToString, generateComparison, generateDefaultConstructor, forbidParameterlessConstruction, 
			generateStaticDefault, propertyName, propertyIsPublic, valueIsNullable, useValidationExceptions)
	{
	}
}

/// <summary>
/// <para>
/// Value objects that have been created by the generator can also be used as underlying type.
/// </para>
/// <para>
/// Value tuples can be used if you need to use multiple underlying values than one:
/// Be sure that 'propertyIsPublic' is set to false and expose the individual parameters by exposing each value of the tuple. 
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public abstract class GenerateValueObjectAttributeBase : Attribute
{
	/// <param name="minimumValue">The minimum value. Default (Int32.MinValue): no minimum.</param>
	/// <param name="maximumValue">The maximum value. Default (Int32.MaxValue): no maximum.</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">Generates comparison operators and CompareTo. Default: true.</param>
	/// <param name="generateDefaultConstructor">Set to true if a default constructor should be generated. If it is false, no default constructor will be generated. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="valueIsNullable">Allow the underlying value to be null. Default: false.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	protected GenerateValueObjectAttributeBase(
		Type? underlyingType,
		int minimumValue = Int32.MinValue, 
		int maximumValue = Int32.MaxValue,
		bool generateToString = true,
		bool generateComparison = true,
		bool generateDefaultConstructor = true,
		bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false,
		string? propertyName = null,
		bool propertyIsPublic = false,
		bool valueIsNullable = false,
		bool useValidationExceptions = true)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

#pragma warning restore IDE0060 // Remove unused parameter
