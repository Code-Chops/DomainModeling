namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Collections;

public class ImmutableDictionaryTests
{
	private record MockId(ulong Value) : Id<MockId, ulong>(Value);
	// ReSharper disable once NotAccessedPositionalProperty.Local
	private record ValueObjectMock(int Value) : IValueObject;
	
	private record Mock(ImmutableDictionary<MockId, ValueObjectMock> Dictionary) : ImmutableDictionaryValueObject<MockId, ValueObjectMock>(Dictionary);

	[Fact]
	public void Dictionary_With_SameKeys_SameValues_ShouldBeEqual()
	{
		var dictionary1 = new Dictionary<MockId, ValueObjectMock>
		{ 
			[new(1)] = new(2),
			[new(2)] = new(3),
		}.ToImmutableDictionary();
		
		var dictionary2 = new Dictionary<MockId, ValueObjectMock>
		{ 
			[new(1)] = new(2),
			[new(2)] = new(3),
		}.ToImmutableDictionary();

		Assert.Equal(new Mock(dictionary1), new Mock(dictionary2));
	}
	
	[Fact]
	public void Dictionary_With_DifferentKeys_SameValues_ShouldNotBeEqual()
	{
		var dictionary1 = new Dictionary<MockId, ValueObjectMock>
		{ 
			[new(1)] = new(2),
			[new(2)] = new(3),
		}.ToImmutableDictionary();
		
		var dictionary2 = new Dictionary<MockId, ValueObjectMock>
		{ 
			[new(2)] = new(2),
			[new(3)] = new(3),
		}.ToImmutableDictionary();

		Assert.NotEqual(new Mock(dictionary1), new Mock(dictionary2));
	}
	
	[Fact]
	public void Dictionary_With_SameKeys_DifferentValues_ShouldNotBeEqual()
	{
		var dictionary1 = new Dictionary<MockId, ValueObjectMock>
		{ 
			[new(1)] = new(2),
			[new(2)] = new(3),
		}.ToImmutableDictionary();
		
		var dictionary2 = new Dictionary<MockId, ValueObjectMock>
		{ 
			[new(1)] = new(3),
			[new(2)] = new(2),
		}.ToImmutableDictionary();

		Assert.NotEqual(new Mock(dictionary1), new Mock(dictionary2));
	}
}