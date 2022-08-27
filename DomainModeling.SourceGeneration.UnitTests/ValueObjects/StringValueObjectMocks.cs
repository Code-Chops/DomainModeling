namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateStringValueObject(addCustomValidation: false)]
public partial struct StringClassStructMock { }

[GenerateStringValueObject(addCustomValidation: false)]
public partial record struct StringRecordStructMock;

[GenerateStringValueObject(addCustomValidation: false)]
public readonly partial struct StringReadonlyStructMock { }

[GenerateStringValueObject(addCustomValidation: false)]
public partial record struct StringReadonlyRecordStructMock;


[GenerateStringValueObject(addCustomValidation: false)]
public partial class StringClassMock { }

[GenerateStringValueObject(addCustomValidation: false)]
public partial record StringRecordClassMock;


[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, generateToString: false, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: true, addCustomValidation: true, generateEmptyStatic: false, compareOptions: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore, propertyName: "Test")]
public partial record struct StringRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, generateToString: false, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: true, addCustomValidation: true, generateEmptyStatic: false, compareOptions: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore, propertyName: "Test")]
public partial record StringRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, generateToString: false, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: true, addCustomValidation: true, generateEmptyStatic: false, compareOptions: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore, propertyName: "Test")]
public sealed partial record StringSealedRecordClassSettingsMock
{
	public void Validate() { }
}