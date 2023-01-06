// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true)]
public partial struct DefaultIntStructMock { }

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true)]
public partial record struct DefaultIntRecordStructMock;

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true)]
public readonly partial struct DefaultIntReadonlyStructMock { }

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true)]
public partial record struct DefaultIntReadonlyRecordStructMock;


[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true)]
public partial class DefaultIntClassMock { }

[GenerateValueObject<int>(minimumValue: Int32.MinValue, maximumValue: Int32.MaxValue, generateToString: true)]
public partial record DefaultIntRecordClassMock;


[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false)]
public partial record struct DefaultIntRecordStructSettingsMock;

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false)]
public partial record DefaultIntRecordClassSettingsMock;

[GenerateValueObject<int>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: true, generateToString: true, propertyName: "Test", valueIsNullable: true, useValidationExceptions: false)]
public sealed partial record DefaultIntSealedRecordClassSettingsMock;
