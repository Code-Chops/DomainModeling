namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateListValueObject<string>]
public partial struct ListStructMock { }

[GenerateListValueObject<string>]
public partial record struct ListRecordStructMock;

[GenerateListValueObject<string>]
public readonly partial struct ListReadonlyStructMock { }

[GenerateListValueObject<string>]
public partial record struct ListReadonlyRecordStructMock;


[GenerateListValueObject<string>]
public partial class ListClassMock { }

[GenerateListValueObject<string>]
public partial record ListRecordClassMock;


[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public partial record struct ListRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public partial record ListRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public sealed partial record ListSealedRecordClassSettingsMock
{
	public void Validate() { }
}