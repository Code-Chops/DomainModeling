namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject(underlyingType: typeof(List<>), propertyIsPublic: true, forbidParameterlessConstruction: true, valueIsNullable: true)]
public readonly partial record struct ParameterSubstitutionMock<TNumber>;
