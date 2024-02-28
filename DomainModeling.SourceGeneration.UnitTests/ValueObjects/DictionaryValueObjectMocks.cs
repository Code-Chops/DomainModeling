// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateDictionaryValueObject<int, string>(generateToString: true, useValidationExceptions: false)]
public partial struct DictionaryClassStructMock;

[GenerateDictionaryValueObject<int, string>(generateToString: true, useValidationExceptions: false)]
public partial record struct DictionaryRecordStructMock;

[GenerateDictionaryValueObject<int, string>(generateToString: true, useValidationExceptions: false)]
public readonly partial struct DictionaryReadonlyStructMock;

[GenerateDictionaryValueObject<int, string>(generateToString: true, useValidationExceptions: false)]
public partial record struct DictionaryReadonlyRecordStructMock;

[GenerateDictionaryValueObject<int, string>(generateToString: true, useValidationExceptions: false)]
public partial class DictionaryClassMock;

[GenerateDictionaryValueObject<int, string>(generateToString: true, useValidationExceptions: false)]
public partial record DictionaryRecordClassMock;

[GenerateDictionaryValueObject<int, string>(minimumCount: 0, maximumCount: 10, generateToString: true, forbidParameterlessConstruction: false, generateStaticDefault: false, propertyName: "Test", generateComparison: false, generateEnumerable: false, generateDefaultConstructor: true, propertyIsPublic: true, valueIsNullable: true, useValidationExceptions: false)]
public partial record struct DictionaryRecordStructSettingsMock;

[GenerateDictionaryValueObject<int, string>(minimumCount: 0, maximumCount: 10, generateToString: true, forbidParameterlessConstruction: false, generateStaticDefault: false, propertyName: "Test", generateComparison: false, generateEnumerable: false, generateDefaultConstructor: true, propertyIsPublic: true, valueIsNullable: true, useValidationExceptions: false)]
public partial record DictionaryRecordClassSettingsMock;

[GenerateDictionaryValueObject<int, string>(minimumCount: 0, maximumCount: 10, generateToString: true, forbidParameterlessConstruction: false, generateStaticDefault: false, propertyName: "Test", generateComparison: false, generateEnumerable: false, generateDefaultConstructor: true, propertyIsPublic: true, valueIsNullable: true, useValidationExceptions: false)]
public sealed partial record DictionarySealedRecordClassSettingsMock;
