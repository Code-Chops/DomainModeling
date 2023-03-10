using CodeChops.DomainModeling.Exceptions.System;
using CodeChops.DomainModeling.Exceptions.Validation;
using CodeChops.DomainModeling.Factories;
using CodeChops.DomainModeling.Validation;
using CodeChops.DomainModeling.Validation.Guards;

namespace CodeChops.DomainModeling.UnitTests.Validation;

public class ValidationTests
{
	[Fact]
	public void Validation_ShouldThrow_OutOfRangeException()
	{
		Assert.Throws<ValidationException<NumberInRangeGuard<int>>>(Initialized);

		static ValidatedObjectMock Initialized()
		{
			var validation = Validator.Get<ValidatedObjectMock>.Default; 
			return new("ThisNameIsTooLong", validation);
		}
	}
	
	[Fact]
	public void Validation_ShouldThrow_KeyNotFoundException()
	{
		Assert.Throws<ValidationException<KeyExistsNoOutputGuard<string>>>(Initialized);

		static ValidatedObjectMock Initialized()
		{
			var validation = Validator.Get<ValidatedObjectMock>.Default; 
			return new("Unknown", validation);
		}
	}
	
	[Fact]
	public void Validation_ShouldThrow_NullException()
	{
		Assert.Throws<ValidationException<NotNullGuard<string>>>(Initialized);

		static ValidatedObjectMock Initialized()
		{
			var validation = Validator.Get<ValidatedObjectMock>.Default; 
			return new(name: null!, validation);
		}
	}
	
	[Fact]
	public void Validation_ShouldThrow_RegexException()
	{
		Assert.Throws<CustomSystemException<RegexGuard>>(() => Initialized());
		
		static Uuid Initialized() => new("12345678901234567890123456789012!");
	}

	[Fact]
	public void Validation_ShouldNotThrow_WhenTryCreate()
	{
		var valid = ICreatable<ValidatedObjectMock, string>.TryCreate("ThisNameIsTooLong", out _);
		
		Assert.False(valid);
	}
	
	[Fact]
	public void Validation_ShouldBeCorrect_WhenTryCreate()
	{
		var valid = ICreatable<ValidatedObjectMock, string>.TryCreate("Max", out _);
		
		Assert.True(valid);
	}
	
	[Fact]
	public void Validation_ShouldThrow_WhenCreate()
	{
		Assert.Throws<ValidationException<NumberInRangeGuard<int>>>(Initialized);

		static void Initialized()
		{
			ValidatedObjectMock.Create("ThisNameIsTooLong", Validator.Get<ValidatedObjectMock>.Default);
		}
	}
}
