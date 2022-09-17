namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

#pragma warning disable CA2231 // Overload operator equals on overriding value type Equals

[GenerateListValueObject<string>(addCustomValidation: false)]
public partial struct ListStructMock { }

[GenerateListValueObject<string>(addCustomValidation: false)]
public partial record struct ListRecordStructMock;

[GenerateListValueObject<string>(addCustomValidation: false)]
public readonly partial struct ListReadonlyStructMock { }

[GenerateListValueObject<string>(addCustomValidation: false)]
public partial record struct ListReadonlyRecordStructMock;


[GenerateListValueObject<string>(addCustomValidation: false)]
public partial class ListClassMock { }

[GenerateListValueObject<string>(addCustomValidation: false)]
public partial record ListRecordClassMock;


[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: false, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public partial record struct ListRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: false, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public partial record ListRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: false, generateDefaultConstructor: true, generateParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public sealed partial record ListSealedRecordClassSettingsMock
{
	public void Validate() { }
}

#pragma warning restore CA2231 // Overload operator equals on overriding value type Equals