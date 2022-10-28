namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<char>(addCustomValidation: false, generateToString: true)]
public partial struct DefaultCharStructMock { }

[GenerateValueObject<char>(addCustomValidation: false, generateToString: true)]
public partial record struct DefaultCharRecordStructMock;

[GenerateValueObject<char>(addCustomValidation: false, generateToString: true)]
public readonly partial struct DefaultCharReadonlyStructMock { }

[GenerateValueObject<char>(addCustomValidation: false, generateToString: true)]
public partial record struct DefaultCharReadonlyRecordStructMock;


[GenerateValueObject<char>(addCustomValidation: false, generateToString: true)]
public partial class DefaultCharClassMock { }

[GenerateValueObject<char>(addCustomValidation: false, generateToString: true)]
public partial record DefaultCharRecordClassMock;


[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true)]
public partial record struct DefaultCharRecordStructSettingsMock
{
	public void Validate() { }
	public DefaultCharRecordStructSettingsMock() { }
}

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true)]
public partial record DefaultCharRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultCharRecordClassSettingsMock() { }
}

[GenerateValueObject<char>(minimumValue: 0, maximumValue: 10, generateDefaultConstructor: true, addParameterlessConstructor: true, generateComparison: false, addCustomValidation: true, generateStaticDefault: true, generateToString: true, propertyName: "Test", allowNull: true)]
public sealed partial record DefaultCharSealedRecordClassSettingsMock
{
	public void Validate() { }
	public DefaultCharSealedRecordClassSettingsMock() { }
}
