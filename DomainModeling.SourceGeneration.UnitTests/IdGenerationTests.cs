﻿using CodeChops.GenericMath;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests;

[GenerateEntityId<byte>]
public partial class EntityWithByteIdMock1 : Entity
{
}

[GenerateEntityId]
public partial class EntityWithByteIdMock2 : Entity
{
}

public class IdGenerationTests
{
	[Fact]
	public void ExplicitId_ShouldBe_Generated()
	{
		var entity = new EntityWithByteIdMock1();
		Assert.Equal(typeof(byte), ((INumber)entity.Id.GetValue()).GetIntegralType());
	}
	
	[Fact]
	public void ImplicitId_ShouldBe_Generated()
	{
		var entity = new EntityWithByteIdMock2();
		Assert.Equal(typeof(uint), ((INumber)entity.Id.GetValue()).GetIntegralType());
	}

}