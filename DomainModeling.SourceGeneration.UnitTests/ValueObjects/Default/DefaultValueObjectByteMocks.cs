namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<byte>(addCustomValidation: false)]
public partial struct DefaultByteStructMock { }

[GenerateValueObject<byte>(addCustomValidation: false)]
public partial record struct DefaultByteRecordStructMock;

[GenerateValueObject<byte>(addCustomValidation: false)]
public readonly partial struct DefaultByteReadonlyStructMock { }

[GenerateValueObject<byte>(addCustomValidation: false)]
public partial record struct DefaultByteReadonlyRecordStructMock;


[GenerateValueObject<byte>(addCustomValidation: false)]
public partial class DefaultByteClassMock { }

[GenerateValueObject<byte>(addCustomValidation: false)]
public partial record DefaultByteRecordClassMock;


[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: false, propertyName: "Test", allowNull: true)]
public partial record struct DefaultByteRecordStructSettingsMock
{
	public void Validate() { }
	public DefaultByteRecordStructSettingsMock() { }
}

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: false, propertyName: "Test", allowNull: true)]
public partial record DefaultByteRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultByteRecordClassSettingsMock() { }
}

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: false, propertyName: "Test", allowNull: true)]
public sealed partial record DefaultByteSealedRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultByteSealedRecordClassSettingsMock() { }
}