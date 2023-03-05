// ReSharper disable SuspiciousTypeConversion.Global
namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.Identities;

public class IdGenerationTests
{
	[Fact]
	public void Explicit_Id_ShouldBe_Generated()
	{
		var id = new EntityWithByteIdMock1Id();
		Assert.Equal(typeof(byte), id.Value.GetType());
	}
	
	[Fact]
	public void Implicit_Id_ShouldBe_Generated()
	{
		var entityId = new EntityWithByteIdMock2Id();
		var classId = new ClassWithIdId();
		Assert.Equal(typeof(ulong), entityId.Value.GetType());
		Assert.Equal(typeof(ulong), classId.Value.GetType());
	}
	
	[Fact]
	public void ClassWithGenericType_Id_ShouldBe_Generated()
	{
		var entity = new ClassWithGenericTypeId();
		Assert.Equal(typeof(ulong), entity.Value.GetType());
	}
	
	[Fact]
	public void Id_WithoutValue_ShouldBe_Default()
	{
		IId id1 = new EntityWithByteIdMock1Id();
		IId id2 = new EntityWithByteIdMock2Id();
		IId id3 = new ClassWithIdId();
		IId id4 = new ClassWithGenericTypeId();
		IId id5 = new ClassWithUuidId();
		
		Assert.True(id1.HasDefaultValue);
		Assert.True(id2.HasDefaultValue);
		Assert.True(id3.HasDefaultValue);
		Assert.True(id4.HasDefaultValue);
		Assert.True(id5.HasDefaultValue);
	}

	[Fact]
	public void Id_WithValue_ShouldNotBe_Default()
	{
		IId id1 = new EntityWithByteIdMock1Id(1);
		IId id2 = new EntityWithByteIdMock2Id(2);
		IId id3 = new ClassWithIdId(3);
		IId id4 = new ClassWithGenericTypeId(4);
		IId id5 = new ClassWithUuidId(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		
		Assert.False(id1.HasDefaultValue);
		Assert.False(id2.HasDefaultValue);
		Assert.False(id3.HasDefaultValue);
		Assert.False(id4.HasDefaultValue);
		Assert.False(id5.HasDefaultValue);
	}

	[Fact]
	public void Ids_WithDifferentValue_ShouldNotBe_Equal()
	{
		var id1A = new EntityWithByteIdMock1Id(1);
		var id1B = new EntityWithByteIdMock1Id(2);
		var id2A = new EntityWithByteIdMock2Id(1);
		var id2B = new EntityWithByteIdMock2Id(2);
		var id3A = new ClassWithIdId(1);
		var id3B = new ClassWithIdId(2);
		var id4A = new ClassWithGenericTypeId(1);
		var id4B = new ClassWithGenericTypeId(2);
		var id5A = new ClassWithUuidId(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		var id5B = new ClassWithUuidId(new Uuid("3AD110A01D304B4593B4D44680DE152D"));
		
		Assert.True(!id1A.Equals(id1B) && id1A != id1B);
		Assert.True(!id2A.Equals(id2B) && id2A != id2B);
		Assert.True(!id3A.Equals(id3B) && id3A != id3B);
		Assert.True(!id4A.Equals(id4B) && id4A != id4B);
		Assert.True(!id5A.Equals(id5B) && id5A != id5B);
	}
	
	[Fact]
	public void Ids_OfDifferentType_ShouldNotBe_Equal()
	{
		var id1 = new EntityWithByteIdMock1Id(1);
		var id2 = new EntityWithByteIdMock2Id(1);
		var id3 = new ClassWithIdId(1);
		var id4 = new ClassWithGenericTypeId(1);
		var id5 = new ClassWithUuidId(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		
		Assert.False(id1.Equals(id2));
		Assert.False(id2.Equals(id3));
		Assert.False(id3.Equals(id4));
		Assert.False(id4.Equals(id5));
	}

	[Fact]
	public void IdenticalIds_ShouldBe_Equal()
	{
		var id1A = new EntityWithByteIdMock1Id(1);
		var id1B = new EntityWithByteIdMock1Id(1);
		var id2A = new EntityWithByteIdMock2Id(1);
		var id2B = new EntityWithByteIdMock2Id(1);
		var id3A = new ClassWithIdId(1);
		var id3B = new ClassWithIdId(1);
		var id4A = new ClassWithGenericTypeId(1);
		var id4B = new ClassWithGenericTypeId(1);
		var id5A = new ClassWithUuidId(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		var id5B = new ClassWithUuidId(new Uuid("2FD110A01D304B4593B4D44680DE152C"));
		
		Assert.True(id1A.Equals(id1B) && id1A == id1B);
		Assert.True(id2A.Equals(id2B) && id2A == id2B);
		Assert.True(id3A.Equals(id3B) && id3A == id3B);
		Assert.True(id4A.Equals(id4B) && id4A == id4B);
		Assert.True(id5A.Equals(id5B) && id5A == id5B);
	}
	
	[Fact]
	public void DefaultIds_ShouldBe_Equal()
	{
		var id1A = new EntityWithByteIdMock1Id();
		var id1B = new EntityWithByteIdMock1Id();
		var id2A = new EntityWithByteIdMock2Id();
		var id2B = new EntityWithByteIdMock2Id();
		var id3A = new ClassWithIdId();
		var id3B = new ClassWithIdId();
		var id4A = new ClassWithGenericTypeId();
		var id4B = new ClassWithGenericTypeId();
		var id5A = new ClassWithUuidId();
		var id5B = new ClassWithUuidId();
		
		Assert.True(id1A.Equals(id1B) && id1A == id1B);
		Assert.True(id2A.Equals(id2B) && id2A == id2B);
		Assert.True(id3A.Equals(id3B) && id3A == id3B);
		Assert.True(id4A.Equals(id4B) && id4A == id4B);
		Assert.True(id5A.Equals(id5B) && id5A == id5B);
	}
	
	[Fact]
	public void DefaultIds_OfDifferentTypes_ShouldNotBe_Equal()
	{
		var id1 = new EntityWithByteIdMock1Id();
		var id2 = new EntityWithByteIdMock2Id();
		var id3 = new ClassWithIdId();
		var id4 = new ClassWithGenericTypeId();
		var id5 = new ClassWithUuidId();
		
		Assert.False(id1.Equals(id2));
		Assert.False(id2.Equals(id3));
		Assert.False(id3.Equals(id4));
		Assert.False(id4.Equals(id5));
	}
}
