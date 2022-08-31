namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<int>(addCustomValidation: false)]
public partial struct DefaultIntStructMock { }

[GenerateValueObject<int>(addCustomValidation: false)]
public partial record struct DefaultIntRecordStructMock;

[GenerateValueObject<int>(addCustomValidation: false)]
public readonly partial struct DefaultIntReadonlyStructMock { }

[GenerateValueObject<int>(addCustomValidation: false)]
public partial record struct DefaultIntReadonlyRecordStructMock;


[GenerateValueObject<int>(addCustomValidation: false)]
public partial class DefaultIntClassMock { }

[GenerateValueObject<int>(addCustomValidation: false)]
public partial record DefaultIntRecordClassMock;


[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test", allowNull: true)]
public partial record struct DefaultIntRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test", allowNull: true)]
public partial record DefaultIntRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test", allowNull: true)]
public sealed partial record DefaultIntSealedRecordClassSettingsMock
{
	public void Validate() { }
}