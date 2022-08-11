namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests;

[GenerateStronglyTypedId<byte>]
public partial class EntityWithByteIdMock1 : Entity
{
}

[GenerateStronglyTypedId]
public partial class EntityWithByteIdMock2 : Entity
{
}

[GenerateStronglyTypedId]
public partial record RecordWithId : IValueObject
{
}

[GenerateStronglyTypedId]
public partial record ClassWithGenericType<T>: IValueObject
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
		var entity = new RecordWithId();
		Assert.Equal(typeof(ulong), entity.Id.Value.GetType());
	}
	
	[Fact]
	public void ClassWithGenericType_Id_ShouldBe_Generated()
	{
		var entity = new ClassWithGenericType<int>();
		Assert.Equal(typeof(ulong), entity.Id.Value.GetType());
	}
}