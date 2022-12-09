// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateListValueObject<string>(generateToString: true, useValidationExceptions: false)]
public partial struct ListStructMock { }

[GenerateListValueObject<string>(generateToString: true, useValidationExceptions: false)]
public partial record struct ListRecordStructMock;

[GenerateListValueObject<string>(generateToString: true, useValidationExceptions: false)]
public readonly partial struct ListReadonlyStructMock { }

[GenerateListValueObject<string>(generateToString: true, useValidationExceptions: false)]
public partial record struct ListReadonlyRecordStructMock;


[GenerateListValueObject<string>(generateToString: true, useValidationExceptions: false)]
public partial class ListClassMock { }

[GenerateListValueObject<string>(generateToString: true, useValidationExceptions: false)]
public partial record ListRecordClassMock;


[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: true, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: false, propertyName: "Test", generateEnumerable: false, propertyIsPublic: true, valueIsNullable: true, useValidationExceptions: false)]
public partial record struct ListRecordStructSettingsMock;

[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: true, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: false, propertyName: "Test", generateEnumerable: false, propertyIsPublic: true, valueIsNullable: true, useValidationExceptions: false)]
public partial record ListRecordClassSettingsMock;

[GenerateListValueObject<string>(minimumCount: 0, maximumCount: 10, generateToString: true, generateDefaultConstructor: true, forbidParameterlessConstruction: false, generateComparison: false, generateStaticDefault: false, propertyName: "Test", propertyIsPublic: true, valueIsNullable: true, useValidationExceptions: false)]
public sealed partial record ListSealedRecordClassSettingsMock;
