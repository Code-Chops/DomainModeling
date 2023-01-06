namespace CodeChops.DomainModeling.UnitTests;

public class ValueObjectClassTests
{
	private class ValueObjectMock : ValueObjectClass<ValueObjectMock>
	{
		public override string ToString() => "";
		public override bool Equals(ValueObjectMock? other) => other is not null && this.Value.Equals(other.Value);
		public override int GetHashCode() => this.Value.GetHashCode();
		public int Value { get; }

		public ValueObjectMock(int value)
		{
			this.Value = value;
		}
	}

	[Fact]
	public void DifferentValueObjects_WithSameValue_ShouldBe_Equal()
	{
		var object1 = new ValueObjectMock(7);
		var object2 = new ValueObjectMock(7);
		
		Assert.Equal(object1, object2);
		Assert.True(object1 == object2);
	}
	
	[Fact]
	public void DifferentValueObjects_WithDifferentValue_ShouldNotBe_Equal()
	{
		var object1 = new ValueObjectMock(6);
		var object2 = new ValueObjectMock(7);
		
		Assert.NotEqual(object1, object2);
		Assert.False(object1 == object2);
	}
	
	[Fact]
	public void SameValueObjects_ShouldBe_Equal()
	{
		var object1 = new ValueObjectMock(6);
		var object2 = object1;
		
		Assert.Equal(object1, object2);
		Assert.True(object1 == object2);
	}
}
