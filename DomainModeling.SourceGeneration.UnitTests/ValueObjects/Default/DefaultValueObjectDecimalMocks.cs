namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<decimal>(addCustomValidation: false)]
public partial struct DefaultDecimalStructMock { }

[GenerateValueObject<decimal>(addCustomValidation: false)]
public partial record struct DefaultDecimalRecordStructMock;

[GenerateValueObject<decimal>(addCustomValidation: false)]
public readonly partial struct DefaultDecimalReadonlyStructMock { }

[GenerateValueObject<decimal>(addCustomValidation: false)]
public partial record struct DefaultDecimalReadonlyRecordStructMock;


[GenerateValueObject<decimal>(addCustomValidation: false)]
public partial class DefaultDecimalClassMock { }

[GenerateValueObject<decimal>(addCustomValidation: false)]
public partial record DefaultDecimalRecordClassMock;


[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test", allowNull: true)]
public partial record struct DefaultDecimalRecordStructSettingsMock
{
	public void Validate() { }
	public DefaultDecimalRecordStructSettingsMock() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test", allowNull: true)]
public partial record DefaultDecimalRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultDecimalRecordClassSettingsMock() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test", allowNull: true)]
public sealed partial record DefaultDecimalSealedRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultDecimalSealedRecordClassSettingsMock() { }
}