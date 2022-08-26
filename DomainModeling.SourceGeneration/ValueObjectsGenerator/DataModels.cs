using System.Globalization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator;

public abstract record ValueObject(
	string Name, 
	string? Namespace, 
	string TypeName,
	string Declaration,
	bool GenerateToString,
	bool GenerateEmptyStatic);

public record StringValueObject(
	string Name,
	string? Namespace, 
	string Declaration,
	bool GenerateToString,
	bool GenerateEmptyStatic,
	bool GenerateEnumerable,
	int? MinimumLength,
	int? MaximumLength,
	StringCaseConversion StringCaseConversion,
	StringFormat StringFormat,
	CompareOptions CompareOptions) 
	: ValueObject(Name, Namespace, nameof(String), Declaration, GenerateToString, GenerateEmptyStatic);

public record IntegralValueObject(
	string Name, 
	string? Namespace, 
	bool GenerateToString,
	bool GenerateEmptyStatic,
	string TypeName,
	string Declaration,
	int? MinimumValue,
	int? MaximumValue) 
	: ValueObject(Name, Namespace, TypeName, Declaration, GenerateToString, GenerateEmptyStatic);

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