﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060

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
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">
	/// Generates Equals(), GetHashCode(), comparison operators (and CompareTo() if possible).
	/// Disable when other properties exist.
	/// Default: true.
	/// </param>
	/// <param name="addCustomValidation">Forces to create a Validate method so custom validation can be implemented. Default: true.</param>
	/// <param name="generateDefaultConstructor">Generates a default constructor. Default: true.</param>
	/// <param name="addParameterlessConstructor">
	/// If true, it forces to create a parameterless constructor (in order to define a default value).
	/// If false, it creates an obsolete parameterless private constructor that throws an exception.
	/// Default: false.
	/// </param>
	/// <param name="generateEmptyStatic">Generates a static property with a default value. Default: false.</param>
	/// <param name="generateEnumerable">Generates an IEnumerable implementation and indexer.</param>
	/// <param name="propertyName">The name of the property. Default: Value.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable).</param>
	/// <param name="minimumCount">The minimum count in the collection. Default: 0.</param>
	/// <param name="maximumCount">The maximum count in the collection. Default: no maximum count.</param>
	// ReSharper disable always UnusedParameter.Local
	public GenerateDictionaryValueObjectAttribute(
		bool generateToString = true,
		bool generateComparison = true,
		bool addCustomValidation = true,
		bool generateDefaultConstructor = true,
		bool addParameterlessConstructor = false, 
		bool generateEmptyStatic = false,
		bool generateEnumerable = true,
		bool propertyIsPublic = false,
		string? propertyName = null,
		int minimumCount = 0, 
		int maximumCount = Int32.MinValue)
	{
		// These parameters will be read from the attribute arguments itself and therefore don't need to be assigned.
	}
}

#pragma warning restore IDE0060