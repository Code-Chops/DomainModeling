namespace CodeChops.DomainModeling.UnitTests;

public class EntityTests
{
	private record IdMock : Id<IdMock, int>
	{
		public IdMock(int value) : base(value) { }
	}

	private class EntityMock1 : Entity<IdMock>
	{
		public EntityMock1(IdMock id) : base(id)
		{
		}
	}
	
	private class EntityMock2 : Entity<IdMock>
	{
		public EntityMock2(IdMock id) : base(id)
		{
		}
	}
	
	[Fact]
	public void Entities_WithSameId_ShouldBe_Equal()
	{
		var entity1 = new EntityMock1(new IdMock(1));
		var entity2 = new EntityMock1(new IdMock(1));
		
		Assert.Equal(entity1, entity2);
		Assert.True(entity1 == entity2);
	}
	
	[Fact]
	public void Entities_WithDifferentIds_ShouldNotBe_Equal()
	{
		var entity1 = new EntityMock1(new IdMock(1));
		var entity2 = new EntityMock1(new IdMock(2));
		
		Assert.NotEqual(entity1, entity2);
		Assert.False(entity1 == entity2);
	}
	
	[Fact]
	public void Entities_OfDifferentType_ShouldNotBe_Equal()
	{
		var entity1 = new EntityMock1(new IdMock(1));
		var entity2 = new EntityMock2(new IdMock(1));
		
		Assert.NotEqual(entity1, (IEntity)entity2);
		Assert.False(entity1 == entity2);
	}
	
	[Fact]
	public void DifferentEntities_WithDefaultIds_ShouldNotBe_Equal()
	{
		var entity1 = new EntityMock1(null!);
		var entity2 = new EntityMock2(null!);
		
		Assert.NotEqual(entity1, (IEntity)entity2);
		Assert.False(entity1 == entity2);
	}
	
	[Fact]
	public void SameEntities_WithDefaultIds_ShouldBe_Equal()
	{
		var entity1 = new EntityMock1(null!);
		var entity2 = entity1;
		
		Assert.Equal(entity1, entity2);
		Assert.True(entity1 == entity2);
	}
}
