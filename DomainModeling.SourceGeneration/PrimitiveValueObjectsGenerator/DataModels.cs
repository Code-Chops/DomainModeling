using System.Globalization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.PrimitiveValueObjectsGenerator;

public abstract record ValueObject(
	string Name, 
	string? Namespace, 
	string PrimitiveTypeName,
	string Declaration,
	bool GenerateToString);

public record StringValueObject(
	string Name,
	string? Namespace, 
	string Declaration,
	int? MinimumLength,
	int? MaximumLength,
	StringCaseConversion StringCaseConversion,
	StringFormat StringFormat,
	bool GenerateEmptyStatic,
	bool GenerateEnumerable,
	bool GenerateToString,
	CompareOptions CompareOptions) 
	: ValueObject(Name, Namespace, nameof(String), Declaration, GenerateToString);

public record IntegralValueObject(
	string Name, 
	string? Namespace, 
	string PrimitiveTypeName,
	string Declaration,
	int? MinimumValue,
	int? MaximumValue, 
	bool GenerateToString) 
	: ValueObject(Name, Namespace, PrimitiveTypeName, Declaration, GenerateToString);

/// <summary>
/// DO NOT RENAME!
/// </summary>
public enum StringCaseConversion
{
	NoConversion,
	LowerInvariant,
	UpperInvariant,
}

public enum StringFormat
{
	Default,
	Alpha,
	AlphaWithUnderscore,
	AlphaNumeric,
	AlphaNumericWithUnderscore,
}