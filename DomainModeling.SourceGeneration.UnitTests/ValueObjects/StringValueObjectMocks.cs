// ReSharper disable UnusedParameterInPartialMethod

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

/// <summary>
/// This is a comment.
/// </summary>
[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, useRegex: false, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, generateToString: true, useValidationExceptions: false)]
public partial struct StringClassStructMock { }

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, useRegex: false, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, generateToString: true, useValidationExceptions: false)]
public partial record struct StringRecordStructMock;

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, useRegex: false, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, generateToString: true, useValidationExceptions: false)]
public readonly partial struct StringReadonlyStructMock { }

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, useRegex: false, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, generateToString: true, useValidationExceptions: false)]
public partial record struct StringReadonlyRecordStructMock;


[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, useRegex: false, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, generateToString: true, useValidationExceptions: false)]
public partial class StringClassMock { }

[GenerateStringValueObject(minimumLength: Int32.MinValue, maximumLength: Int32.MaxValue, useRegex: false, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, generateToString: true, useValidationExceptions: false)]
public partial record StringRecordClassMock;


[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, useRegex: false, generateToString: true, generateDefaultConstructor: true, forbidParameterlessConstruction: true, generateComparison: false, generateStaticDefault: false, stringComparison: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore, propertyName: "Test", valueIsNullable: true, generateEnumerable: false, propertyIsPublic: true, useValidationExceptions: false)]
public partial record struct StringRecordStructSettingsMock;

[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, useRegex: false, generateToString: true, generateDefaultConstructor: true, forbidParameterlessConstruction: true, generateComparison: false, generateStaticDefault: false, stringComparison: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore, propertyName: "Test", valueIsNullable: true, generateEnumerable: false, propertyIsPublic: true, useValidationExceptions: false)]
public partial record StringRecordClassSettingsMock;

[GenerateStringValueObject(minimumLength: 0, maximumLength: 10, useRegex: false, generateToString: true, generateDefaultConstructor: true, forbidParameterlessConstruction: true, generateComparison: false, generateStaticDefault: false, stringComparison: StringComparison.Ordinal, stringCaseConversion: StringCaseConversion.UpperInvariant, stringFormat: StringFormat.AlphaNumericWithUnderscore, propertyName: "Test", valueIsNullable: true, generateEnumerable: false, propertyIsPublic: true, useValidationExceptions: false)]
public sealed partial record StringSealedRecordClassSettingsMock;
