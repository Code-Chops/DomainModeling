namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateDictionaryValueObject<int, string>]
public partial struct DictionaryClassStructMock { }

[GenerateDictionaryValueObject<int, string>]
public partial record struct DictionaryRecordStructMock;

[GenerateDictionaryValueObject<int, string>]
public readonly partial struct DictionaryReadonlyStructMock { }

[GenerateDictionaryValueObject<int, string>]
public partial record struct DictionaryReadonlyRecordStructMock;


[GenerateDictionaryValueObject<int, string>]
public partial class DictionaryClassMock { }

[GenerateDictionaryValueObject<int, string>]
public partial record DictionaryRecordClassMock;


[GenerateDictionaryValueObject<int, string>(minimumCount: 0, maximumCount: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public partial record struct DictionaryRecordStructSettingsMock
{
	public void Validate() { }
}

[GenerateDictionaryValueObject<int, string>(minimumCount: 0, maximumCount: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public partial record DictionaryRecordClassSettingsMock
{
	public void Validate() { }
}

[GenerateDictionaryValueObject<int, string>(minimumCount: 0, maximumCount: 10, generateToString: false, prohibitParameterlessConstruction: false, addCustomValidation: true, generateEmptyStatic: false, propertyName: "Test")]
public sealed partial record DictionarySealedRecordClassSettingsMock
{
	public void Validate() { }
}