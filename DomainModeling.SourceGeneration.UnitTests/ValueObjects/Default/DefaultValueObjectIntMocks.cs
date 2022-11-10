// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true)]
public partial struct DefaultIntStructMock { }

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true)]
public partial record struct DefaultIntRecordStructMock;

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true)]
public readonly partial struct DefaultIntReadonlyStructMock { }

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true)]
public partial record struct DefaultIntReadonlyRecordStructMock;


[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true)]
public partial class DefaultIntClassMock { }

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true)]
public partial record DefaultIntRecordClassMock;


[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record struct DefaultIntRecordStructSettingsMock
{
	private partial void Validate(Validator validator) { }
	public DefaultIntRecordStructSettingsMock() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record DefaultIntRecordClassSettingsMock
{
	private partial void Validate(Validator validator) { }
	public DefaultIntRecordClassSettingsMock() { }
}

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public sealed partial record DefaultIntSealedRecordClassSettingsMock
{
	private partial void Validate(Validator validator) { }
	public DefaultIntSealedRecordClassSettingsMock() { }
}
