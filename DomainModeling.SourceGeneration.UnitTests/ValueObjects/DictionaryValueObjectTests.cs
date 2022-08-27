namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

internal record MockId(ulong Value) : Id<MockId, ulong>(Value);
// ReSharper disable once NotAccessedPositionalProperty.Local
internal record DictionaryValueObjectMock(int Value) : IValueObject;

[GenerateDictionaryValueObject<MockId, ValueObjectMock>]
internal partial record ImmutableDictionaryMock;

public class DictionaryValueObjectTests
{
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

		Assert.Equal(new ImmutableDictionaryMock(dictionary1), new ImmutableDictionaryMock(dictionary2));
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

		Assert.NotEqual(new ImmutableDictionaryMock(dictionary1), new ImmutableDictionaryMock(dictionary2));
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

		Assert.NotEqual(new ImmutableDictionaryMock(dictionary1), new ImmutableDictionaryMock(dictionary2));
	}
}