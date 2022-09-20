namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.Identities;

[GenerateStronglyTypedId<byte>]
public partial class EntityWithByteIdMock1 : Entity
{
}

[GenerateStronglyTypedId]
public partial class EntityWithByteIdMock2 : Entity
{
}

[GenerateStronglyTypedId]
public partial class ClassWithId
{
}

[GenerateStronglyTypedId]
// ReSharper disable once UnusedTypeParameter
public partial class ClassWithGenericType<T>
{
}

[GenerateStronglyTypedId<string>(typeof(Guid<>))]
public partial class ClassWithGuidId
{
}

[GenerateStronglyTypedId<(string, string)>(typeof(TupleId<,>))]
public partial class ClassWithTupleId
{
}

public class IdGenerationTests
{
	[Fact]
	public void Explicit_Id_ShouldBe_Generated()
	{
		var entity = new EntityWithByteIdMock1();
		Assert.Equal(typeof(byte), entity.Id.Value.GetType());
	}
	
	[Fact]
	public void Implicit_Id_ShouldBe_Generated()
	{
		var entity = new EntityWithByteIdMock2();
		Assert.Equal(typeof(ulong), entity.Id.Value.GetType());
	}
	
	[Fact]
	public void Record_Id_ShouldBe_Generated()
	{
		var entity = new ClassWithId();
		Assert.Equal(typeof(ulong), entity.Id.Value.GetType());
	}
	
	[Fact]
	public void ClassWithGenericType_Id_ShouldBe_Generated()
	{
		var entity = new ClassWithGenericType<int>();
		Assert.Equal(typeof(ulong), entity.Id.Value.GetType());
	}
}