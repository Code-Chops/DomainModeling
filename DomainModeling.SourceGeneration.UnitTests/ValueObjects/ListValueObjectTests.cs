namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

// ReSharper disable once NotAccessedPositionalProperty.Local
internal record ValueObjectMock(int Value) : IValueObject;

[GenerateListValueObject<ValueObjectMock>(addCustomValidation: false, generateToString: true)]
internal partial record ListMock;

public class ListValueObjectTests
{
	[Fact]
	public void Lists_With_SameValues_ShouldBeEqual()
	{
		var list1 = new List<ValueObjectMock>
		{ 
			new(2),
			new(3),
		}.ToImmutableList();
		
		var list2 = new List<ValueObjectMock>
		{ 
			new(2),
			new(3),
		}.ToImmutableList();

		Assert.Equal(new ListMock(list1), new ListMock(list2));
	}
	
	[Fact]
	public void Lists_With_DifferentValues_ShouldNotBeEqual()
	{
		var list1 = new List<ValueObjectMock>
		{ 
			new(2),
			new(3),
		}.ToImmutableList();
		
		var list2 = new List<ValueObjectMock>
		{ 
			new(3),
			new(2),
		}.ToImmutableList();

		Assert.NotEqual(new ListMock(list1), new ListMock(list2));
	}
}
