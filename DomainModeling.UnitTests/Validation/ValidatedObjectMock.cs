using CodeChops.DomainDrivenDesign.DomainModeling.Factories;
using CodeChops.DomainDrivenDesign.DomainModeling.Validation;
using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Validation;

public class ValidatedObjectMock : IDomainObject, ICreatable<ValidatedObjectMock, string>
{
	public Dictionary<string, int> AgeByName { get; } = new() { ["Max"] = 7 };
	
	public string Name { get; }
	
	public ValidatedObjectMock(string name, ref Validator<ValidatedObjectMock> validator)
	{
		var errorCode = new ErrorCodeMock();
		
		validator
			.GuardNotNull(name, errorCode)
			.GuardInRange<int>(name.Length, 1, 10, errorCode)
			.GuardKeyExists(this.AgeByName.ContainsKey, name, errorCode);
		
		this.Name = name;
	}

	public static ValidatedObjectMock Create(string name, Validator<ValidatedObjectMock> validator)
		=> new(name, ref validator);
}
