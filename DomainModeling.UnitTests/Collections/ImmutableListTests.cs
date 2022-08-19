namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Collections;

public class ImmutableListTests
{
	// ReSharper disable once NotAccessedPositionalProperty.Local
	private record ValueObjectMock(int Value) : IValueObject;
	
	private record Mock(ImmutableList<ValueObjectMock> List) : ImmutableListValueObject<ValueObjectMock>(List);

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

		Assert.Equal(new Mock(list1), new Mock(list2));
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

		Assert.NotEqual(new Mock(list1), new Mock(list2));
	}
}