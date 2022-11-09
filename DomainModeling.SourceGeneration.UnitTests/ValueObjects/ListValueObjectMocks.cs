// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial struct ListStructMock { }

[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct ListRecordStructMock;

[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public readonly partial struct ListReadonlyStructMock { }

[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct ListReadonlyRecordStructMock;


[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial class ListClassMock { }

[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record ListRecordClassMock;


[GenerateListValueObject<string>(
	minimumCount: 0, 
	maximumCount: 10, 
	generateToString: true, 
	constructorIsPublic: true,
	forbidParameterlessConstruction: false,
	generateComparison: false,
	addCustomValidation: true,
	generateStaticDefault: false,
	propertyName: "Test",
	generateEnumerable: false,
	propertyIsPublic: true, 
	allowNull: true,
	useValidationExceptions: false)]
public partial record struct ListRecordStructSettingsMock
{
	private partial void Validate(Validator<ListRecordStructSettingsMock> validator) { }
}

[GenerateListValueObject<string>(
	minimumCount: 0, 
	maximumCount: 10, 
	generateToString: true, 
	constructorIsPublic: true, 
	forbidParameterlessConstruction: false, 
	generateComparison: false, 
	addCustomValidation: true, 
	generateStaticDefault: false,
	propertyName: "Test", 
	generateEnumerable: false,
	propertyIsPublic: true, 
	allowNull: true,
	useValidationExceptions: false)]
public partial record ListRecordClassSettingsMock
{
	private partial void Validate(Validator<ListRecordClassSettingsMock> validator) { }
}

[GenerateListValueObject<string>(
	minimumCount: 0, 
	maximumCount: 10, 
	generateToString: true, 
	constructorIsPublic: true, 
	forbidParameterlessConstruction: false, 
	generateComparison: false, 
	addCustomValidation: true, 
	generateStaticDefault: false, 
	propertyName: "Test",
	propertyIsPublic: true, 
	allowNull: true,
	useValidationExceptions: false)]
public sealed partial record ListSealedRecordClassSettingsMock
{
	private partial void Validate(Validator<ListSealedRecordClassSettingsMock> validator) { }
}
