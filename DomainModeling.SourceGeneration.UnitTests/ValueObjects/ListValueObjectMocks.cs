namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

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


[GenerateListValueObject<string>(
	minimumCount: 0, 
	maximumCount: 10, 
	generateToString: false, 
	generateDefaultConstructor: true,
	addParameterlessConstructor: true,
	generateComparison: false,
	addCustomValidation: true,
	generateStaticDefault: false,
	propertyName: "Test",
	generateEnumerable: false,
	propertyIsPublic: true)]
public partial record struct ListRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateListValueObject<string>(
	minimumCount: 0, 
	maximumCount: 10, 
	generateToString: false, 
	generateDefaultConstructor: true, 
	addParameterlessConstructor: true, 
	generateComparison: false, 
	addCustomValidation: true, 
	generateStaticDefault: false,
	propertyName: "Test", 
	generateEnumerable: false,
	propertyIsPublic: true)]
public partial record ListRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateListValueObject<string>(
	minimumCount: 0, 
	maximumCount: 10, 
	generateToString: false, 
	generateDefaultConstructor: true, 
	addParameterlessConstructor: true, 
	generateComparison: false, 
	addCustomValidation: true, 
	generateStaticDefault: false, 
	propertyName: "Test",
	propertyIsPublic: true)]
public sealed partial record ListSealedRecordClassSettingsMock
{
	public void Validate() { }
}