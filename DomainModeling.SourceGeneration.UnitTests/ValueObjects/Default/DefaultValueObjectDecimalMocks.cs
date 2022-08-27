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


[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: true, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test")]
public partial record struct DefaultDecimalRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: true, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test")]
public partial record DefaultDecimalRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: true, addCustomValidation: true, generateEmptyStatic: true, generateToString: false, propertyName: "Test")]
public sealed partial record DefaultDecimalSealedRecordClassSettingsMock
{
	public void Validate() { }
}