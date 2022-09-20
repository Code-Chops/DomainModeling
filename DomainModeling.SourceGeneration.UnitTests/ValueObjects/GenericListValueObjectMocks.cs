namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

[GenerateListValueObject<string>(addCustomValidation: false)]
public partial struct GenericListMock<T> { }

[GenerateListValueObject<string>(addCustomValidation: false)]
public partial struct GenericListWithConstraintsMock<T> where T : class { }
