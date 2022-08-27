namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<byte>]
public partial struct DefaultByteStructMock { }

[GenerateValueObject<byte>]
public partial record struct DefaultByteRecordStructMock;

[GenerateValueObject<byte>]
public readonly partial struct DefaultByteReadonlyStructMock { }

[GenerateValueObject<byte>]
public partial record struct DefaultByteReadonlyRecordStructMock;


[GenerateValueObject<byte>]
public partial class DefaultByteClassMock { }

[GenerateValueObject<byte>]
public partial record DefaultByteRecordClassMock;


[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public partial record struct DefaultByteRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public partial record DefaultByteRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public sealed partial record DefaultByteSealedRecordClassSettingsMock
{
	public void Validate() { }
}