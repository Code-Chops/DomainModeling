using CodeChops.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests;

public class EntityTests
{
	private record IdMock : Id<IdMock, int>
	{
		public IdMock(int value) : base(value) { }
		public IdMock() { }
	}

	private class EntityMock1 : Entity
	{
		public override IdMock Id { get; }
		public EntityMock1(IdMock? id = null) { this.Id = id ?? new IdMock(); }
	}
	
	private class EntityMock2 : Entity
	{
		public override IdMock Id { get; }
		public EntityMock2(IdMock? id = null) { this.Id = id ?? new IdMock(); }
	}
	
	[Fact]
	public void Entities_WithSameId_ShouldBe_Equal()
	{
		var id1 = new EntityMock1(new IdMock(1));
		var id2 = new EntityMock1(new IdMock(1));
		
		Assert.Equal(id1, id2);
	}
	
	[Fact]
	public void Entities_WithDifferentIds_ShouldNotBe_Equal()
	{
		var id1 = new EntityMock1(new IdMock(1));
		var id2 = new EntityMock1(new IdMock(2));
		
		Assert.NotEqual(id1, id2);
	}
	
	[Fact]
	public void Entities_OfDifferentType_ShouldNotBe_Equal()
	{
		var id1 = new EntityMock1(new IdMock(1));
		var id2 = new EntityMock2(new IdMock(1));
		
		Assert.NotEqual((Entity)id1, (Entity)id2);
	}
	
	[Fact]
	public void DifferentEntities_WithDefaultIds_ShouldNotBe_Equal()
	{
		var id1 = new EntityMock1();
		var id2 = new EntityMock1();
		
		Assert.NotEqual(id1, id2);
	}
	
	[Fact]
	public void SameEntities_WithDefaultIds_ShouldBe_Equal()
	{
		var id1 = new EntityMock1();
		var id2 = id1;
		
		Assert.Equal(id1, id2);
	}
}