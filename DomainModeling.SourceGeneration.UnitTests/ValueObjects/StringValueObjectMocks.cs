namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateStringValueObject(addCustomValidation: false, generateToString: true)]
public partial struct StringClassStructMock { }

[GenerateStringValueObject(addCustomValidation: false, generateToString: true)]
public partial record struct StringRecordStructMock;

[GenerateStringValueObject(addCustomValidation: false, generateToString: true)]
public readonly partial struct StringReadonlyStructMock { }

[GenerateStringValueObject(addCustomValidation: false, generateToString: true)]
public partial record struct StringReadonlyRecordStructMock;


[GenerateStringValueObject(addCustomValidation: false, generateToString: true)]
public partial class StringClassMock { }

[GenerateStringValueObject(addCustomValidation: false, generateToString: true)]
public partial record StringRecordClassMock;


[GenerateStringValueObject(
	minimumLength: 0, 
	maximumLength: 10, 
	generateToString: true, 
	generateDefaultConstructor: true, 
	addParameterlessConstructor: true, 
	generateComparison: false, 
	addCustomValidation: true,
	generateStaticDefault: false,
	compareOptions: StringComparison.Ordinal,
	stringCaseConversion: StringCaseConversion.UpperInvariant,
	stringFormat: StringFormat.AlphaNumericWithUnderscore,
	propertyName: "Test",
	allowNull: true,
	generateEnumerable: false,
	propertyIsPublic: true)]
public partial record struct StringRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateStringValueObject(
	minimumLength: 0, 
	maximumLength: 10, 
	generateToString: true,
	generateDefaultConstructor: true, 
	addParameterlessConstructor: true,
	generateComparison: false,
	addCustomValidation: true,
	generateStaticDefault: false,
	compareOptions: StringComparison.Ordinal,
	stringCaseConversion: StringCaseConversion.UpperInvariant,
	stringFormat: StringFormat.AlphaNumericWithUnderscore,
	propertyName: "Test", 
	allowNull: true,
	generateEnumerable: false,
	propertyIsPublic: true)]
public partial record StringRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateStringValueObject(
	minimumLength: 0, 
	maximumLength: 10, 
	generateToString: true, 
	generateDefaultConstructor: true,
	addParameterlessConstructor: true,
	generateComparison: false, 
	addCustomValidation: true,
	generateStaticDefault: false,
	compareOptions: StringComparison.Ordinal, 
	stringCaseConversion: StringCaseConversion.UpperInvariant,
	stringFormat: StringFormat.AlphaNumericWithUnderscore,
	propertyName: "Test",
	allowNull: true, 
	generateEnumerable: false,
	propertyIsPublic: true)]
public sealed partial record StringSealedRecordClassSettingsMock
{
	public void Validate() { }
}
