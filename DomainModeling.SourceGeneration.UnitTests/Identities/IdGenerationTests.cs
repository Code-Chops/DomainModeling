// ReSharper disable SuspiciousTypeConversion.Global
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

[GenerateStronglyTypedId<Uuid>]
public partial class ClassWithUuid
{
}

public class IdGenerationTests
{
	[Fact]
	public void Explicit_Id_ShouldBe_Generated()
	{
		var id = new EntityWithByteIdMock1.Identity();
		Assert.Equal(typeof(byte), id.GetValue().GetType());
	}
	
	[Fact]
	public void Implicit_Id_ShouldBe_Generated()
	{
		var entityId = new EntityWithByteIdMock2.Identity();
		var classId = new ClassWithId.Identity();
		Assert.Equal(typeof(ulong), entityId.GetValue().GetType());
		Assert.Equal(typeof(ulong), classId.GetValue().GetType());
	}
	
	[Fact]
	public void ClassWithGenericType_Id_ShouldBe_Generated()
	{
		var entity = new ClassWithGenericType<int>.Identity();
		Assert.Equal(typeof(ulong), entity.GetValue().GetType());
	}
	
	[Fact]
	public void Id_WithoutValue_ShouldBe_Default()
	{
		var id1 = new EntityWithByteIdMock1.Identity();
		var id2 = new EntityWithByteIdMock2.Identity();
		var id3 = new ClassWithId.Identity();
		var id4 = new ClassWithGenericType<int>.Identity();
		var id5 = new ClassWithUuid.Identity();
		
		Assert.True(id1.HasDefaultValue);
		Assert.True(id2.HasDefaultValue);
		Assert.True(id3.HasDefaultValue);
		Assert.True(id4.HasDefaultValue);
		Assert.True(id5.HasDefaultValue);
	}

	[Fact]
	public void Id_WithValue_ShouldNotBe_Default()
	{
		var id1 = new EntityWithByteIdMock1.Identity(1);
		var id2 = new EntityWithByteIdMock2.Identity(2);
		var id3 = new ClassWithId.Identity(3);
		var id4 = new ClassWithGenericType<int>.Identity(4);
		var id5 = new ClassWithUuid.Identity(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		
		Assert.False(id1.HasDefaultValue);
		Assert.False(id2.HasDefaultValue);
		Assert.False(id3.HasDefaultValue);
		Assert.False(id4.HasDefaultValue);
		Assert.False(id5.HasDefaultValue);
	}

	[Fact]
	public void Ids_WithDifferentValue_ShouldNotBe_Equal()
	{
		var id1A = new EntityWithByteIdMock1.Identity(1);
		var id1B = new EntityWithByteIdMock1.Identity(2);
		var id2A = new EntityWithByteIdMock2.Identity(1);
		var id2B = new EntityWithByteIdMock2.Identity(2);
		var id3A = new ClassWithId.Identity(1);
		var id3B = new ClassWithId.Identity(2);
		var id4A = new ClassWithGenericType<int>.Identity(1);
		var id4B = new ClassWithGenericType<int>.Identity(2);
		var id5A = new ClassWithUuid.Identity(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		var id5B = new ClassWithUuid.Identity(new Uuid("3AD110A01D304B4593B4D44680DE152D"));
		
		Assert.True(!id1A.Equals(id1B) && id1A != id1B);
		Assert.True(!id2A.Equals(id2B) && id2A != id2B);
		Assert.True(!id3A.Equals(id3B) && id3A != id3B);
		Assert.True(!id4A.Equals(id4B) && id4A != id4B);
		Assert.True(!id5A.Equals(id5B) && id5A != id5B);
	}
	
	[Fact]
	public void Ids_OfDifferentType_ShouldNotBe_Equal()
	{
		var id1 = new EntityWithByteIdMock1.Identity(1);
		var id2 = new EntityWithByteIdMock2.Identity(1);
		var id3 = new ClassWithId.Identity(1);
		var id4 = new ClassWithGenericType<int>.Identity(1);
		var id5 = new ClassWithUuid.Identity(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		
		Assert.False(id1.Equals(id2));
		Assert.False(id2.Equals(id3));
		Assert.False(id3.Equals(id4));
		Assert.False(id4.Equals(id5));
	}

	[Fact]
	public void IdenticalIds_ShouldBe_Equal()
	{
		var id1A = new EntityWithByteIdMock1.Identity(1);
		var id1B = new EntityWithByteIdMock1.Identity(1);
		var id2A = new EntityWithByteIdMock2.Identity(1);
		var id2B = new EntityWithByteIdMock2.Identity(1);
		var id3A = new ClassWithId.Identity(1);
		var id3B = new ClassWithId.Identity(1);
		var id4A = new ClassWithGenericType<int>.Identity(1);
		var id4B = new ClassWithGenericType<int>.Identity(1);
		var id5A = new ClassWithUuid.Identity(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		var id5B = new ClassWithUuid.Identity(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		
		Assert.True(id1A.Equals(id1B) && id1A == id1B);
		Assert.True(id2A.Equals(id2B) && id2A == id2B);
		Assert.True(id3A.Equals(id3B) && id3A == id3B);
		Assert.True(id4A.Equals(id4B) && id4A == id4B);
		Assert.True(id5A.Equals(id5B) && id5A == id5B);
	}
	
	[Fact]
	public void DefaultIds_ShouldBe_Equal()
	{
		var id1A = new EntityWithByteIdMock1.Identity();
		var id1B = new EntityWithByteIdMock1.Identity();
		var id2A = new EntityWithByteIdMock2.Identity();
		var id2B = new EntityWithByteIdMock2.Identity();
		var id3A = new ClassWithId.Identity();
		var id3B = new ClassWithId.Identity();
		var id4A = new ClassWithGenericType<int>.Identity();
		var id4B = new ClassWithGenericType<int>.Identity();
		var id5A = new ClassWithUuid.Identity();
		var id5B = new ClassWithUuid.Identity();
		
		Assert.True(id1A.Equals(id1B) && id1A == id1B);
		Assert.True(id2A.Equals(id2B) && id2A == id2B);
		Assert.True(id3A.Equals(id3B) && id3A == id3B);
		Assert.True(id4A.Equals(id4B) && id4A == id4B);
		Assert.True(id5A.Equals(id5B) && id5A == id5B);
	}
	
	[Fact]
	public void DefaultIds_OfDifferentTypes_ShouldNotBe_Equal()
	{
		var id1 = new EntityWithByteIdMock1.Identity();
		var id2 = new EntityWithByteIdMock2.Identity();
		var id3 = new ClassWithId.Identity();
		var id4 = new ClassWithGenericType<int>.Identity();
		var id5 = new ClassWithUuid.Identity();
		
		Assert.False(id1.Equals(id2));
		Assert.False(id2.Equals(id3));
		Assert.False(id3.Equals(id4));
		Assert.False(id4.Equals(id5));
	}
}