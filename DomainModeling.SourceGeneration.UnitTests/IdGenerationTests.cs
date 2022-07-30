﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests;

[GenerateStronglyTypedId<byte>]
public partial class EntityWithByteIdMock1 : Entity
{
}

[GenerateStronglyTypedId]
public partial class EntityWithByteIdMock2 : Entity
{
}

public class IdGenerationTests
{
	[Fact]
	public void ExplicitId_ShouldBe_Generated()
	{
		var entity = new EntityWithByteIdMock1();
		Assert.Equal(typeof(byte), entity.Id.Value.GetType());
	}
	
	[Fact]
	public void ImplicitId_ShouldBe_Generated()
	{
		var entity = new EntityWithByteIdMock2();
		Assert.Equal(typeof(ulong), entity.Id.Value.GetType());
	}
}