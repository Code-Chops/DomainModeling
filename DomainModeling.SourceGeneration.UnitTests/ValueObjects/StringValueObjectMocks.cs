namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateStringValueObject]
public partial struct StringClassStructMock { }

[GenerateStringValueObject]
public partial record struct StringRecordStructMock;

[GenerateStringValueObject]
public readonly partial struct StringReadonlyStructMock { }

[GenerateStringValueObject]
public partial record struct StringReadonlyRecordStructMock;


[GenerateStringValueObject]
public partial class StringClassMock { }

[GenerateStringValueObject]
public partial record StringRecordClassMock;


[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, compareOptions: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore)]
public partial record struct StringRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, compareOptions: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore)]
public partial record StringRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, compareOptions: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore)]
public sealed partial record StringSealedRecordClassSettingsMock
{
	public void Validate() { }
}