namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<decimal>]
public partial struct DefaultDecimalStructMock { }

[GenerateValueObject<decimal>]
public partial record struct DefaultDecimalRecordStructMock;

[GenerateValueObject<decimal>]
public readonly partial struct DefaultDecimalReadonlyStructMock { }

[GenerateValueObject<decimal>]
public partial record struct DefaultDecimalReadonlyRecordStructMock;


[GenerateValueObject<decimal>]
public partial class DefaultDecimalClassMock { }

[GenerateValueObject<decimal>]
public partial record DefaultDecimalRecordClassMock;


[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false)]
public partial record struct DefaultDecimalRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false)]
public partial record DefaultDecimalRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, generateToString: false)]
public sealed partial record DefaultDecimalSealedRecordClassSettingsMock
{
	public void Validate() { }
}