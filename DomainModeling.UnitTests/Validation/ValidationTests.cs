using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;
using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;
using CodeChops.DomainDrivenDesign.DomainModeling.Validation;
using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Validation;

public class ValidationTests
{
	[Fact]
	public void Validation_ShouldThrow_OutOfRangeException()
	{
		Assert.Throws<ValidationException<InRangeNoOutputGuard<int>>>(Initialized);

		static ValidatedObjectMock Initialized()
		{
			var validation = new Validator<ValidatedObjectMock>(); 
			return new("ThisNameIsTooLong", ref validation);
		}
	}
	
	[Fact]
	public void Validation_ShouldThrow_KeyNotFoundException()
	{
		Assert.Throws<ValidationException<KeyExistsNoOutputGuard<string>>>(Initialized);

		static ValidatedObjectMock Initialized()
		{
			var validation = new Validator<ValidatedObjectMock>(); 
			return new("Unknown", ref validation);
		}
	}
	
	[Fact]
	public void Validation_ShouldThrow_NullException()
	{
		Assert.Throws<ValidationException<NotNullGuard<string>>>(Initialized);

		static ValidatedObjectMock Initialized()
		{
			var validation = new Validator<ValidatedObjectMock>(); 
			return new(name: null!, ref validation);
		}
	}
	
	[Fact]
	public void Validation_ShouldThrow_RegexException()
	{
		Assert.Throws<SystemException<RegexGuard>>(() => Initialized());
		
		static Uuid Initialized() => new("12345678901234567890123456789012!");
	}

	[Fact]
	public void Validation_ShouldNotThrow_Exception_WhenTryCreate()
	{
		var validation = new Validator<ValidatedObjectMock>(throwWhenInvalid: false);
		var _ = new ValidatedObjectMock("ThisNameIsTooLong", ref validation);
		
		Assert.NotNull(validation.CurrentExceptions);
		Assert.True(!validation.IsValid);
		Assert.Equal(nameof(ValidatedObjectMock), validation.ObjectName);
	}
}
