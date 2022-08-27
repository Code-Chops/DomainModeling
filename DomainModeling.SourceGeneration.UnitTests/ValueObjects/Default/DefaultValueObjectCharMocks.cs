namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<char>]
public partial struct DefaultCharStructMock { }

[GenerateValueObject<char>]
public partial record struct DefaultCharRecordStructMock;

[GenerateValueObject<char>]
public readonly partial struct DefaultCharReadonlyStructMock { }

[GenerateValueObject<char>]
public partial record struct DefaultCharReadonlyRecordStructMock;


[GenerateValueObject<char>]
public partial class DefaultCharClassMock { }

[GenerateValueObject<char>]
public partial record DefaultCharRecordClassMock;


[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public partial record struct DefaultCharRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public partial record DefaultCharRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public sealed partial record DefaultCharSealedRecordClassSettingsMock
{
	public void Validate() { }
}