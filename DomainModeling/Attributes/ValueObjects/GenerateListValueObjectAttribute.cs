namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// Generates a value object with a single structural value of type ImmutableList&lt;<typeparamref name="TElement"/>&gt;.
/// <inheritdoc cref="GenerateListValueObjectAttributeBase"/>
/// </summary>
/// <typeparam name="TElement">The type of the list elements.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
// ReSharper disable once UnusedTypeParameter
public sealed class GenerateListValueObjectAttribute<TElement> : GenerateListValueObjectAttributeBase
{
	/// <inheritdoc />
	public GenerateListValueObjectAttribute(int minimumCount = Int32.MinValue, int maximumCount = Int32.MaxValue, bool generateEnumerable = true, 
		bool generateToString = true, bool generateComparison = true, bool generateDefaultConstructor = true, bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false, string? propertyName = null, bool propertyIsPublic = false, bool valueIsNullable = false, bool useValidationExceptions = true) 
		: base(elementType: typeof(TElement), minimumCount, maximumCount, generateEnumerable, generateToString, generateComparison, generateDefaultConstructor, 
			forbidParameterlessConstruction, generateStaticDefault, propertyName, propertyIsPublic, valueIsNullable, useValidationExceptions)
	{
	}
}

/// <summary>
/// Generates a value object with a single structural value of type List with elements of the provided type.
/// <inheritdoc cref="GenerateListValueObjectAttributeBase"/>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
// ReSharper disable once UnusedTypeParameter
public sealed class GenerateListValueObjectAttribute : GenerateListValueObjectAttributeBase
{
	/// <inheritdoc />
	public GenerateListValueObjectAttribute(Type? elementType, int minimumCount = Int32.MinValue, int maximumCount = Int32.MaxValue, bool generateEnumerable = true, 
		bool generateToString = true, bool generateComparison = true, bool generateDefaultConstructor = true, bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false, string? propertyName = null, bool propertyIsPublic = false, bool valueIsNullable = false, bool useValidationExceptions = true) 
		: base(elementType, minimumCount, maximumCount, generateEnumerable, generateToString, generateComparison, generateDefaultConstructor, 
			forbidParameterlessConstruction, generateStaticDefault, propertyName, propertyIsPublic, valueIsNullable, useValidationExceptions)
	{
	}
}

/// <summary>
/// <para>Any manually extra added property is not being used for calculating Equals or GetHashCode.</para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
// ReSharper disable once UnusedTypeParameter
public abstract class GenerateListValueObjectAttributeBase : Attribute
{
	/// <param name="minimumCount">The minimum element count in the collection. Default (Int32.MinValue): no minimum.</param>
	/// <param name="maximumCount">The maximum element count in the collection. Default (Int32.MaxValue): no maximum.</param>
	/// <param name="generateEnumerable">Generates an IEnumerable implementation and indexer. Default: true.</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">Generates comparison operators and CompareTo. Default: true.</param>
	/// <param name="generateDefaultConstructor">Set to true if a default constructor should be generated. If it is false, no default constructor will be generated. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generate a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="valueIsNullable">Allow the element value to be null. Default: false.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	protected GenerateListValueObjectAttributeBase(
		Type? elementType,
		int minimumCount = Int32.MinValue, 
		int maximumCount = Int32.MaxValue,
		bool generateEnumerable = true,
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
