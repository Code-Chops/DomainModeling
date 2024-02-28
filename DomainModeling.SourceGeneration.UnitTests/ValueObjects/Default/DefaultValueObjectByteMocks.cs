// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial struct DefaultByteStructMock;

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultByteRecordStructMock;

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public readonly partial struct DefaultByteReadonlyStructMock;

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultByteReadonlyRecordStructMock;


[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial class DefaultByteClassMock;

[GenerateValueObject<byte>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record DefaultByteRecordClassMock;


[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public partial record struct DefaultByteRecordStructSettingsMock(byte? Test, Validator? Validator = null);

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public partial record DefaultByteRecordClassSettingsMock(byte? Test, Validator? Validator = null);

[GenerateValueObject<byte>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public sealed partial record DefaultByteSealedRecordClassSettingsMock(byte? Test, Validator? Validator = null);
