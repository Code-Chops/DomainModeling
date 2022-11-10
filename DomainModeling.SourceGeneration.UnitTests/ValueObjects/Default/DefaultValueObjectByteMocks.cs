// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial struct DefaultByteStructMock { }

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultByteRecordStructMock;

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public readonly partial struct DefaultByteReadonlyStructMock { }

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultByteReadonlyRecordStructMock;


[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial class DefaultByteClassMock { }

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
public partial record DefaultByteRecordClassMock;


[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record struct DefaultByteRecordStructSettingsMock
{
	private partial void Validate(Validator validator) { }
	public DefaultByteRecordStructSettingsMock() { }
}

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public partial record DefaultByteRecordClassSettingsMock
{
	private partial void Validate(Validator validator) { }
	public DefaultByteRecordClassSettingsMock() { }
}

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, constructorIsPublic: true, forbidParameterlessConstruction: false, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true, useValidationExceptions: false)]
public sealed partial record DefaultByteSealedRecordClassSettingsMock
{
	private partial void Validate(Validator validator) { }
	public DefaultByteSealedRecordClassSettingsMock() { }
}
