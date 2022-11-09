// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial struct DefaultCharStructMock { }

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultCharRecordStructMock;

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public readonly partial struct DefaultCharReadonlyStructMock { }

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultCharReadonlyRecordStructMock;


[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial class DefaultCharClassMock { }

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record DefaultCharRecordClassMock;


[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record struct DefaultCharRecordStructSettingsMock
{
	private partial void Validate(Validator<DefaultCharRecordStructSettingsMock> validator) { }
	public DefaultCharRecordStructSettingsMock() { }
}

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record DefaultCharRecordClassSettingsMock
{
	private partial void Validate(Validator<DefaultCharRecordClassSettingsMock> validator) { }
	public DefaultCharRecordClassSettingsMock() { }
}

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public sealed partial record DefaultCharSealedRecordClassSettingsMock
{
	private partial void Validate(Validator<DefaultCharSealedRecordClassSettingsMock> validator) { }
	public DefaultCharSealedRecordClassSettingsMock() { }
}
