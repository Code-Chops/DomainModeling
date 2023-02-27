// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial struct DefaultDecimalStructMock { }

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultDecimalRecordStructMock;

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public readonly partial struct DefaultDecimalReadonlyStructMock { }

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record struct DefaultDecimalReadonlyRecordStructMock;


[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial class DefaultDecimalClassMock { }

[GenerateValueObject<decimal>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true, useValidationExceptions: false)]
public partial record DefaultDecimalRecordClassMock;


[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public partial record struct DefaultDecimalRecordStructSettingsMock(decimal? Test, Validator? Validator = null);

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public partial record DefaultDecimalRecordClassSettingsMock(decimal? Test, Validator? Validator = null);

[GenerateValueObject<decimal>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false, propertyIsPublic: true, useCustomProperty: true)]
public sealed partial record DefaultDecimalSealedRecordClassSettingsMock(decimal? Test, Validator? Validator = null);
