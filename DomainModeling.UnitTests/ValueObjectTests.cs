namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests;

public class ValueObjectTests
{
	// ReSharper disable once NotAccessedPositionalProperty.Local
	private record ValueObjectMock(int Value) : IValueObject;

	[Fact]
	public void DifferentValueObjects_WithSameValue_ShouldBe_Equal()
	{
		var object1 = new ValueObjectMock(7);
		var object2 = new ValueObjectMock(7);
		
		Assert.Equal(object1, object2);
	}
	
	[Fact]
	public void DifferentValueObjects_WithDifferentValue_ShouldNotBe_Equal()
	{
		var object1 = new ValueObjectMock(6);
		var object2 = new ValueObjectMock(7);
		
		Assert.NotEqual(object1, object2);
	}
	
	[Fact]
	public void SameValueObjects_ShouldBe_Equal()
	{
		var object1 = new ValueObjectMock(6);
		var object2 = object1;
		
		Assert.Equal(object1, object2);
	}
}