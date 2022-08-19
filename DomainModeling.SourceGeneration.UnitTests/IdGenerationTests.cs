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
public partial record RecordWithId;

[GenerateStronglyTypedId]
// ReSharper disable once UnusedTypeParameter
public partial record RecordWithGenericType<T>;

[GenerateStronglyTypedId<string>(typeof(Guid<>))]
public partial record RecordWithGuidId;

[GenerateStronglyTypedId<(string, string)>(typeof(TupleId<,>))]
public partial record RecordWithTupleId;


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
		var entity = new RecordWithGenericType<int>();
		Assert.Equal(typeof(ulong), entity.Id.Value.GetType());
	}
}