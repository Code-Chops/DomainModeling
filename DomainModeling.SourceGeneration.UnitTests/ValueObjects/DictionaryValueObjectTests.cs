using System.Diagnostics.CodeAnalysis;

namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects;

internal record MockId : Id<MockId, ulong>
{
	[SetsRequiredMembers]
	public MockId(ulong value) 
	{
		this.Value = value;
	}
}

// ReSharper disable once NotAccessedPositionalProperty.Local
internal record DictionaryValueObjectMock(int Value) : IValueObject;

[GenerateDictionaryValueObject<MockId, ValueObjectMock>(generateToString: true, useValidationExceptions: false)]
internal partial record DictionaryMock;

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

		Assert.Equal(new DictionaryMock(dictionary1), new DictionaryMock(dictionary2));
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

		Assert.NotEqual(new DictionaryMock(dictionary1), new DictionaryMock(dictionary2));
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

		Assert.NotEqual(new DictionaryMock(dictionary1), new DictionaryMock(dictionary2));
	}
}
