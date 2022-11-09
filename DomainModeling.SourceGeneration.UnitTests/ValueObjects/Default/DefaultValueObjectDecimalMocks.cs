// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial struct DefaultDecimalStructMock { }

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultDecimalRecordStructMock;

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public readonly partial struct DefaultDecimalReadonlyStructMock { }

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultDecimalReadonlyRecordStructMock;


[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial class DefaultDecimalClassMock { }

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record DefaultDecimalRecordClassMock;


[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record struct DefaultDecimalRecordStructSettingsMock
{
	private partial void Validate(Validator<DefaultDecimalRecordStructSettingsMock> validator) { }
	public DefaultDecimalRecordStructSettingsMock() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record DefaultDecimalRecordClassSettingsMock
{
	private partial void Validate(Validator<DefaultDecimalRecordClassSettingsMock> validator) { }
	public DefaultDecimalRecordClassSettingsMock() { }
}

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public sealed partial record DefaultDecimalSealedRecordClassSettingsMock
{
	private partial void Validate(Validator<DefaultDecimalSealedRecordClassSettingsMock> validator) { }
	public DefaultDecimalSealedRecordClassSettingsMock() { }
}
