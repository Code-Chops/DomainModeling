namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

/// <summary>
/// Generates a value object with a single structural value of type Dictionary&lt;<typeparamref name="TKey"/>, <typeparamref name="TValue"/>&gt;.
/// </summary>
/// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
/// <typeparam name="TValue">The type of the dictionary values.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
// ReSharper disable once UnusedTypeParameter
public sealed class GenerateDictionaryValueObjectAttribute<TKey, TValue> : Attribute
	where TKey : IComparable<TKey>
{
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="prohibitParameterlessConstruction">Creates an obsolete parameterless private constructor that throws an exception. Structs can still be instantiated by using default(). Default: true.</param>
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: false.</param>
	/// <param name="generateEmptyStatic">Generate a static property with a default value. Default: true.</param>
	/// <param name="minimumCount">The minimum count in the collection. Default: no minimum count.</param>
	/// <param name="maximumCount">The maximum count in the collection. Default: no maximum count.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateDictionaryValueObjectAttribute(
		bool generateToString = true,
		bool prohibitParameterlessConstruction = true, 
		bool addCustomValidation = false,
		bool generateEmptyStatic = true,
		int minimumCount = Int32.MinValue, 
		int maximumCount = Int32.MinValue)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}