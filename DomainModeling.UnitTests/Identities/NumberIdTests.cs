namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Identities;

public class NumberIdTests
{
	private record IntIdMock : Id<IntIdMock, int>
	{
		public IntIdMock(int value) : base(value) { }
		public IntIdMock() { }
	}
	
	private record UIntIdMock : Id<UIntIdMock, uint>
	{
		public UIntIdMock(uint value) : base(value) { }
		public UIntIdMock() { }
	}
	
	[Fact]
	public void Id_WithoutValue_ShouldBe_Default()
	{
		var guid = new IntIdMock();
		
		Assert.True(guid.HasDefaultValue);
	}

	[Fact]
	public void Id_WithValue_ShouldNotBe_Default()
	{
		var guid = new IntIdMock(1);
		
		Assert.False(guid.HasDefaultValue);
	}

	[Fact]
	public void Ids_WithDifferentValue_ShouldNotBe_Equal()
	{
		var id1 = new IntIdMock(6);
		var id2 = new IntIdMock(7);
		
		Assert.NotEqual(id1, id2);
	}
	
	[Fact]
	public void Ids_OfDifferentType_ShouldNotBe_Equal()
	{
		var id1 = (Id)new IntIdMock(7);
		var id2 = (Id)new UIntIdMock(7);
		
		Assert.False(id1 == id2);
		Assert.NotEqual(id1, id2);
	}

	[Fact]
	public void IdenticalIds_ShouldBe_Equal()
	{
		var id1 = new IntIdMock(7);
		var id2 = new IntIdMock(7);
		
		Assert.Equal(id1, id2);
		Assert.True(id1 == id2);
	}
	
	[Fact]
	public void DefaultIds_ShouldBe_Equal()
	{
		var id1 = new IntIdMock();
		var id2 = new IntIdMock();
		
		Assert.Equal(id1, id2);
		Assert.True(id1 == id2);
	}
	
	[Fact]
	public void DefaultIds_OfDifferentTypes_ShouldNotBe_Equal()
	{
		var id1 = (Id)new IntIdMock();
		var id2 = (Id)new UIntIdMock();
		
		Assert.False(id1 == id2);
		Assert.NotEqual(id1, id2);
	}
}