﻿namespace CodeChops.DomainModeling.Attributes.ValueObjects;

#pragma warning disable IDE0060 // Remove unused parameter

/// <summary>
/// <para>Generates a value object with a single structural value of type ImmutableDictionary&lt;<typeparamref name="TKey"/>, <typeparamref name="TValue"/>&gt;.</para>
/// <inheritdoc cref="GenerateDictionaryValueObjectAttributeBase"/>
/// </summary>
/// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
/// <typeparam name="TValue">The type of the dictionary values.</typeparam>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateDictionaryValueObjectAttribute<TKey, TValue> : GenerateDictionaryValueObjectAttributeBase
{
	/// <inheritdoc />
	public GenerateDictionaryValueObjectAttribute(int minimumCount = Int32.MinValue, int maximumCount = Int32.MaxValue, bool generateEnumerable = true, 
		bool generateToString = true, bool generateComparison = true, bool generateDefaultConstructor = true, bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false, string? propertyName = null, bool propertyIsPublic = false, bool valueIsNullable = false, bool useValidationExceptions = true) 
		: base(keyType: typeof(TKey), valueType: typeof(TValue), minimumCount, maximumCount, 
		generateEnumerable, generateToString, generateComparison, generateDefaultConstructor, forbidParameterlessConstruction, generateStaticDefault, propertyName, 
		propertyIsPublic, valueIsNullable, useValidationExceptions)
	{
	}
}

/// <summary>
/// <para>Generates a value object with a single structural value of an immutable dictionary with a provided key and value type.</para>
/// <inheritdoc cref="GenerateDictionaryValueObjectAttributeBase"/>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
public sealed class GenerateDictionaryValueObjectAttribute : GenerateDictionaryValueObjectAttributeBase 
{
	/// <inheritdoc />
	public GenerateDictionaryValueObjectAttribute(Type? keyType, Type? valueType, int minimumCount = Int32.MinValue, int maximumCount = Int32.MaxValue, bool generateEnumerable = true, 
		bool generateToString = true, bool generateComparison = true, bool generateDefaultConstructor = true, bool forbidParameterlessConstruction = true, 
		bool generateStaticDefault = false, string? propertyName = null, bool propertyIsPublic = false, bool valueIsNullable = false, bool useValidationExceptions = true) 
		: base(keyType: keyType, valueType: valueType, minimumCount, maximumCount, 
			generateEnumerable, generateToString, generateComparison, generateDefaultConstructor, forbidParameterlessConstruction, generateStaticDefault, propertyName, 
			propertyIsPublic, valueIsNullable, useValidationExceptions)
	{
	}
}

/// <summary>
/// <para>Any manually extra added property is not being used for calculating Equals or GetHashCode.</para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
// ReSharper disable once UnusedTypeParameter
public abstract class GenerateDictionaryValueObjectAttributeBase : Attribute
{
	/// <param name="minimumCount">The minimum count of KeyValuePairs in the collection. Default (Int32.MinValue): no minimum.</param>
	/// <param name="maximumCount">The maximum count of KeyValuePairs in the collection. Default (Int32.MaxValue): no maximum.</param>
	/// <param name="generateEnumerable">Generates an IEnumerable implementation and indexer. Default: true.</param>
	/// <param name="generateToString">Generates a ToString(). Default: true.</param>
	/// <param name="generateComparison">Generates comparison operators and CompareTo when the underlying value implements IComparable. Default: true.</param>
	/// <param name="generateDefaultConstructor">Set to true if a default constructor should be generated. If it is false, no default constructor will be generated. Default: true.</param>
	/// <param name="forbidParameterlessConstruction">If true (default), it creates an obsolete parameterless private constructor that throws an exception.</param>
	/// <param name="generateStaticDefault">Generates a static property with a default value. Default: false.</param>
	/// <param name="propertyName">The name of the property. Default: 'Value'.</param>
	/// <param name="propertyIsPublic">If true, the generated property will be publicly accessible (not settable). Default: false.</param>
	/// <param name="valueIsNullable">Allow the values to be null. Default: false.</param>
	/// <param name="useValidationExceptions">Throw validation exceptions instead of system exceptions. Default: true.</param>
	// ReSharper disable always UnusedParameter.Local
	protected GenerateDictionaryValueObjectAttributeBase(
		Type? keyType,
		Type? valueType,
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
