﻿using System.Diagnostics.CodeAnalysis;

namespace CodeChops.DomainModeling.UnitTests;

public class EntityTests
{
	private record IdMock : Id<IdMock, int>
	{
		[SetsRequiredMembers]
		public IdMock(int value)
		{
			this.Value = value;
		}
	}

	private class EntityMock1(IdMock id) : Entity<IdMock>(id);

	private class EntityMock2(IdMock id) : Entity<IdMock>(id);

	[Fact]
	public void Entities_WithSameId_ShouldBe_Equal()
	{
		var entity1 = new EntityMock1(id: new IdMock(1));
		var entity2 = new EntityMock1(id: new IdMock(1));

		Assert.Equal(entity1, entity2);
		Assert.True(entity1 == entity2);
	}

	[Fact]
	public void Entities_WithDifferentIds_ShouldNotBe_Equal()
	{
		var entity1 = new EntityMock1(id: new IdMock(1));
		var entity2 = new EntityMock1(id: new IdMock(2));

		Assert.NotEqual(entity1, entity2);
		Assert.False(entity1 == entity2);
	}

	[Fact]
	public void Entities_OfDifferentType_ShouldNotBe_Equal()
	{
		var entity1 = new EntityMock1(id: new IdMock(1));
		var entity2 = new EntityMock2(id: new IdMock(1));

		Assert.NotEqual(entity1, (IEntity)entity2);
		Assert.False(entity1 == entity2);
	}

	[Fact]
	public void DifferentEntities_WithDefaultIds_ShouldNotBe_Equal()
	{
		var entity1 = new EntityMock1(id: new(0));
		var entity2 = new EntityMock2(id: new(0));

		Assert.NotEqual(entity1, (IEntity)entity2);
		Assert.False(entity1 == entity2);
	}

	[Fact]
	public void SameEntities_WithDefaultIds_ShouldBe_Equal()
	{
		var entity1 = new EntityMock1(id: new(0));
		var entity2 = entity1;

		Assert.Equal(entity1, entity2);
		Assert.True(entity1 == entity2);
	}
}
