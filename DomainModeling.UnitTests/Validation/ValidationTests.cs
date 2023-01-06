using CodeChops.DomainModeling.Exceptions.System;
using CodeChops.DomainModeling.Exceptions.Validation;
using CodeChops.DomainModeling.Validation;
using CodeChops.DomainModeling.Validation.Guards;

namespace CodeChops.DomainModeling.UnitTests.Validation;

public class ValidationTests
{
	[Fact]
	public void Validation_ShouldThrow_OutOfRangeException()
	{
		Assert.Throws<ValidationException<InRangeNoOutputGuard<int>>>(Initialized);

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
		Assert.Throws<SystemException<RegexGuard>>(() => Initialized());
		
		static Uuid Initialized() => new("12345678901234567890123456789012!");
	}

	[Fact]
	public void Validation_ShouldNotThrow_WhenTryCreate()
	{
		var valid = ValidatedObjectMock.TryCreate("ThisNameIsTooLong", out var o, out var validation);
		
		Assert.False(valid);
		Assert.Null(o);
		Assert.NotEmpty(validation.CurrentExceptions);
		Assert.False(validation.IsValid);
		Assert.Equal(nameof(ValidatedObjectMock), validation.ObjectName);
	}
	
	[Fact]
	public void Validation_ShouldBeCorrect_WhenTryCreate()
	{
		var valid = ValidatedObjectMock.TryCreate("Max", out var o, out var validation);
		
		Assert.True(valid);
		Assert.NotNull(o);
		Assert.Empty(validation.CurrentExceptions);
		Assert.True(validation.IsValid);
		Assert.Equal(nameof(ValidatedObjectMock), validation.ObjectName);
	}
	
	[Fact]
	public void Validation_ShouldThrow_WhenCreate()
	{
		Assert.Throws<ValidationException<InRangeNoOutputGuard<int>>>(Initialized);

		static void Initialized()
		{
			ValidatedObjectMock.Create("ThisNameIsTooLong", Validator.Get<ValidatedObjectMock>.Default);
		}
	}
}
