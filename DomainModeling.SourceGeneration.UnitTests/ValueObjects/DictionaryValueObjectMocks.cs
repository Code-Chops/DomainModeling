namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true)]
public partial struct DictionaryClassStructMock { }

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true)]
public partial record struct DictionaryRecordStructMock;

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true)]
public readonly partial struct DictionaryReadonlyStructMock { }

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true)]
public partial record struct DictionaryReadonlyRecordStructMock;


[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true)]
public partial class DictionaryClassMock { }

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true)]
public partial record DictionaryRecordClassMock;


[GenerateDictionaryValueObject<int, string>(
	minimumCount: 0,
	maximumCount: 10, 
	generateToString: true,
	addParameterlessConstructor: false,
	addCustomValidation: true, 
	generateStaticDefault: false,
	propertyName: "Test",
	generateComparison: false, 
	generateEnumerable: false,
	generateDefaultConstructor: false,
	propertyIsPublic: true)]
public partial record struct DictionaryRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateDictionaryValueObject<int, string>(
	minimumCount: 0,
	maximumCount: 10, 
	generateToString: true, 
	addParameterlessConstructor: false, 
	addCustomValidation: true,
	generateStaticDefault: false, 
	propertyName: "Test", 
	generateComparison: false, 
	generateEnumerable: false, 
	generateDefaultConstructor: false,
	propertyIsPublic: true)]
public partial record DictionaryRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateDictionaryValueObject<int, string>(
	minimumCount: 0, 
	maximumCount: 10,
	generateToString: true, 
	addParameterlessConstructor: false, 
	addCustomValidation: true, 
	generateStaticDefault: false, 
	propertyName: "Test",
	generateComparison: false, 
	generateEnumerable: false, 
	generateDefaultConstructor: false,
	propertyIsPublic: true)]
public sealed partial record DictionarySealedRecordClassSettingsMock
{
	public void Validate() { }
}
