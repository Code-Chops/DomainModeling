namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.Identities;

[GenerateIdentity<byte>]
public class EntityWithByteIdMock1 : Entity<EntityWithByteIdMock1Id>
{
	public EntityWithByteIdMock1(EntityWithByteIdMock1Id id) 
		: base(id)
	{
	}
}

[GenerateIdentity]
public class EntityWithByteIdMock2 : Entity<EntityWithByteIdMock2Id>
{
	public EntityWithByteIdMock2(EntityWithByteIdMock2Id id) 
		: base(id)
	{
	}
}

[GenerateIdentity]
public record RecordWithId;

[GenerateIdentity]
// ReSharper disable once UnusedTypeParameter
public record RecordWithGenericType<T>;

[GenerateIdentity]
public class ClassWithId
{
}

[GenerateIdentity]
// ReSharper disable once UnusedTypeParameter
public class ClassWithGenericType<T>
{
}

[GenerateIdentity<Uuid>]
public class ClassWithUuid
{
}

[GenerateIdentity<GeneratedValueObjectId>]
public class ClassWithGeneratedValueObjectId
{
}

[GenerateValueObject<int>(minimumValue: 9, maximumValue: Int32.MaxValue, useValidationExceptions: false)]
public readonly partial record struct GeneratedValueObjectId;
