namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
// ReSharper disable once UnusedTypeParameter
public partial struct GenericListMock<T> { }

[GenerateListValueObject<string>(addCustomValidation: false, generateToString: true, useValidationExceptions: false)]
// ReSharper disable once UnusedTypeParameter
public partial struct GenericListWithConstraintsMock<T> where T : class { }
