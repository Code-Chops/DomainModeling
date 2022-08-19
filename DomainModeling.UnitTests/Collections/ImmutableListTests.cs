namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Collections;

public class ImmutableListTests
{
	// ReSharper disable once NotAccessedPositionalProperty.Local
	private record ValueObjectMock(int Value) : IValueObject;
	
	private record ListMock(ImmutableList<ValueObjectMock> List) : ImmutableDomainObjectList<ValueObjectMock>(List);

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