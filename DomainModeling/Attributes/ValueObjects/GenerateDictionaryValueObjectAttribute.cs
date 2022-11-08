namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// Generates a value object with a single structural value of type ImmutableDictionary&lt;<typeparamref name="TKey"/>, <typeparamref name="TValue"/>&gt;.
/// </summary>
/// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
/// <typeparam name="TValue">The type of the dictionary values.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
// ReSharper disable once UnusedTypeParameter
public sealed class GenerateDictionaryValueObjectAttribute<TKey, TValue> : Attribute
	where TKey : IComparable<TKey>
{
	/// <param name="minimumCount">The minimum count in the collection. Default: no minimum (Int32.MinValue).</param>
	/// <param name="maximumCount">The maximum count in the collection. Default: no maximum (Int32.MaxValue).</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="constructorIsPublic">Set to true if the default constructor should be public, if false it will be private. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generates a static property with a default value. Default: false.</param>
	/// <param name="generateEnumerable">Generates an IEnumerable implementation and indexer. Default: true.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="allowNull">Allow the values to be null. Default: false.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateDictionaryValueObjectAttribute(
		int minimumCount = Int32.MinValue, 
		int maximumCount = Int32.MaxValue,
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool constructorIsPublic = true,
		bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false,
		bool generateEnumerable = true,
		string? propertyName = null,
		bool propertyIsPublic = false,
		bool allowNull = false,
		bool useValidationExceptions = true)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

#pragma warning restore IDE0060 // Remove unused parameter
