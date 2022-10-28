namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<int>(addCustomValidation: false, generateToString: true)]
public partial struct DefaultIntStructMock { }

[GenerateValueObject<int>(addCustomValidation: false, generateToString: true)]
public partial record struct DefaultIntRecordStructMock;

[GenerateValueObject<int>(addCustomValidation: false, generateToString: true)]
public readonly partial struct DefaultIntReadonlyStructMock { }

[GenerateValueObject<int>(addCustomValidation: false, generateToString: true)]
public partial record struct DefaultIntReadonlyRecordStructMock;


[GenerateValueObject<int>(addCustomValidation: false, generateToString: true)]
public partial class DefaultIntClassMock { }

[GenerateValueObject<int>(addCustomValidation: false, generateToString: true)]
public partial record DefaultIntRecordClassMock;


[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true)]
public partial record struct DefaultIntRecordStructSettingsMock
{
	public void Validate() { }
	public DefaultIntRecordStructSettingsMock() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true)]
public partial record DefaultIntRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultIntRecordClassSettingsMock() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true)]
public sealed partial record DefaultIntSealedRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultIntSealedRecordClassSettingsMock() { }
}
