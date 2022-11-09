// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial struct StringClassStructMock { }

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct StringRecordStructMock;

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public readonly partial struct StringReadonlyStructMock { }

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct StringReadonlyRecordStructMock;


[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial class StringClassMock { }

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record StringRecordClassMock;


[GenerateStringValueObject(
	minimumLength: 0, 
	maximumLength: 10, 
	generateToString: true, 
	constructorIsPublic: true, 
	forbidParameterlessConstruction: false, 
	generateComparison: false, 
	addCustomValidation: true,
	generateStaticDefault: false,
	stringComparison: StringComparison.Ordinal,
	stringCaseConversion: StringCaseConversion.UpperInvariant,
	stringFormat: StringFormat.AlphaNumericWithUnderscore,
	propertyName: "Test",
	allowNull: true,
	generateEnumerable: false,
	propertyIsPublic: true, 
	useValidationExceptions: false)]
public partial record struct StringRecordStructSettingsMock
{
	private partial void Validate(Validator<StringRecordStructSettingsMock> validator) { }
}

[GenerateStringValueObject(
	minimumLength: 0, 
	maximumLength: 10, 
	generateToString: true,
	constructorIsPublic: true, 
	forbidParameterlessConstruction: false,
	generateComparison: false,
	addCustomValidation: true,
	generateStaticDefault: false,
	stringComparison: StringComparison.Ordinal,
	stringCaseConversion: StringCaseConversion.UpperInvariant,
	stringFormat: StringFormat.AlphaNumericWithUnderscore,
	propertyName: "Test", 
	allowNull: true,
	generateEnumerable: false,
	propertyIsPublic: true, 
	useValidationExceptions: false)]
public partial record StringRecordClassSettingsMock
{
	private partial void Validate(Validator<StringRecordClassSettingsMock> validator) { }
}

[GenerateStringValueObject(
	minimumLength: 0, 
	maximumLength: 10, 
	generateToString: true, 
	constructorIsPublic: true,
	forbidParameterlessConstruction: false,
	generateComparison: false, 
	addCustomValidation: true,
	generateStaticDefault: false,
	stringComparison: StringComparison.Ordinal, 
	stringCaseConversion: StringCaseConversion.UpperInvariant,
	stringFormat: StringFormat.AlphaNumericWithUnderscore,
	propertyName: "Test",
	allowNull: true, 
	generateEnumerable: false,
	propertyIsPublic: true, 
	useValidationExceptions: false)]
public sealed partial record StringSealedRecordClassSettingsMock
{
	private partial void Validate(Validator<StringSealedRecordClassSettingsMock> validator) { }
}
