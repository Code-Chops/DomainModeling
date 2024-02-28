// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial struct DefaultCharStructMock;

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultCharRecordStructMock;

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public readonly partial struct DefaultCharReadonlyStructMock;

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultCharReadonlyRecordStructMock;


[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial class DefaultCharClassMock;

[GenerateValueObject<char>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record DefaultCharRecordClassMock;


[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public partial record struct DefaultCharRecordStructSettingsMock(char? Test, Validator? Validator = null);

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public partial record DefaultCharRecordClassSettingsMock(char? Test, Validator? Validator = null);

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public sealed partial record DefaultCharSealedRecordClassSettingsMock(char? Test, Validator? Validator = null);
