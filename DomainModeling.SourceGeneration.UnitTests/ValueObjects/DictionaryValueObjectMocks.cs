// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial struct DictionaryClassStructMock { }

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DictionaryRecordStructMock;

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public readonly partial struct DictionaryReadonlyStructMock { }

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DictionaryReadonlyRecordStructMock;


[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial class DictionaryClassMock { }

[GenerateDictionaryValueObject<int, string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record DictionaryRecordClassMock;


[GenerateDictionaryValueObject<int, string>(
	minimumCount: 0,
	maximumCount: 10, 
	generateToString: true,
	forbidParameterlessConstruction: false,
	addCustomValidation: true, 
	generateStaticDefault: false,
	propertyName: "Test",
	generateComparison: false, 
	generateEnumerable: false,
	constructorIsPublic: false,
	propertyIsPublic: true, 
	allowNull: true,
	useValidationExceptions: false)]
public partial record struct DictionaryRecordStructSettingsMock
{
	private partial void Validate(Validator<DictionaryRecordStructSettingsMock> validator) { }
}

[GenerateDictionaryValueObject<int, string>(
	minimumCount: 0,
	maximumCount: 10, 
	generateToString: true, 
	forbidParameterlessConstruction: false, 
	addCustomValidation: true,
	generateStaticDefault: false, 
	propertyName: "Test", 
	generateComparison: false, 
	generateEnumerable: false, 
	constructorIsPublic: false,
	propertyIsPublic: true, 
	allowNull: true,
	useValidationExceptions: false)]
public partial record DictionaryRecordClassSettingsMock
{
	private partial void Validate(Validator<DictionaryRecordClassSettingsMock> validator) { }
}

[GenerateDictionaryValueObject<int, string>(
	minimumCount: 0, 
	maximumCount: 10,
	generateToString: true, 
	forbidParameterlessConstruction: false, 
	addCustomValidation: true, 
	generateStaticDefault: false, 
	propertyName: "Test",
	generateComparison: false, 
	generateEnumerable: false, 
	constructorIsPublic: false,
	propertyIsPublic: true, 
	allowNull: true,
	useValidationExceptions: false)]
public sealed partial record DictionarySealedRecordClassSettingsMock
{
	private partial void Validate(Validator<DictionarySealedRecordClassSettingsMock> validator) { }
}
