namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<int>]
public partial struct DefaultIntStructMock { }

[GenerateValueObject<int>]
public partial record struct DefaultIntRecordStructMock;

[GenerateValueObject<int>]
public readonly partial struct DefaultIntReadonlyStructMock { }

[GenerateValueObject<int>]
public partial record struct DefaultIntReadonlyRecordStructMock;


[GenerateValueObject<int>]
public partial class DefaultIntClassMock { }

[GenerateValueObject<int>]
public partial record DefaultIntRecordClassMock;


[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public partial record struct DefaultIntRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public partial record DefaultIntRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false, propertyName: "Test")]
public sealed partial record DefaultIntSealedRecordClassSettingsMock
{
	public void Validate() { }
}