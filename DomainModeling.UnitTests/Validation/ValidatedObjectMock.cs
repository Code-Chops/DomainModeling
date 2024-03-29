﻿using System.Text.RegularExpressions;
using CodeChops.DomainModeling.Factories;
using CodeChops.DomainModeling.Validation;
using CodeChops.DomainModeling.Validation.Guards;

namespace CodeChops.DomainModeling.UnitTests.Validation;

public class ValidatedObjectMock : IDomainObject, ICreatable<ValidatedObjectMock, string>
{
	public Dictionary<string, int> AgeByName { get; } = new() { ["Max"] = 7 };
	
	public string Name { get; }
	
	public ValidatedObjectMock(string name, Validator? validator)
	{
		validator ??= Validator.Get<ValidatedObjectMock>.Default;
		
		var errorCode = new ErrorCodeMock();

		validator.GuardNotNull(name, errorCode);
		validator.GuardInRange(name.Length, 1, 10, errorCode);
		validator.GuardKeyExists(this.AgeByName.ContainsKey, name, errorCode);
		
		this.Name = name;
	}

	public static ValidatedObjectMock Create(string name, Validator? validator = null)
		=> new(name, validator);
}

public enum ErrorCode
{
	Name_IsNull,
	Name_LengthOutOfRange,
	Name_Invalid,
}

public record Name : ValueObject<Name>, ICreatable<Name, string>
{
	public static Name Create(string name, Validator? validator = null)
		=> new(name, validator);

	private Name(string name, Validator? validator = null)
	{
		validator ??= Validator.Get<Name>.Default;
		
		validator.GuardNotNull(name, errorCode: ErrorCode.Name_IsNull.ToString());
		validator.GuardLengthInRange(name, 3, 5, errorCode: ErrorCode.Name_LengthOutOfRange.ToString());
		validator.GuardRegex(name, new Regex("^[a-zA-Z][a-zA-Z0-9]*$", RegexOptions.Compiled, matchTimeout: TimeSpan.FromSeconds(1)), ErrorCode.Name_Invalid.ToString());
	}
}

public record Person(Name Name) : ValueObject<Person>
{
	public bool Exists(string nameString)
	{
		if (!ICreatable<Name, string>.TryCreate(nameString, out _))
			return false;
		
		/* etc */
		return true;
	}
}
