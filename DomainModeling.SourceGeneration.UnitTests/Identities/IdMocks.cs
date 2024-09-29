namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.Identities;

[GenerateIdentity<byte>]
public class EntityWithByteIdMock1(EntityWithByteIdMock1Id id) : Entity<EntityWithByteIdMock1Id>(id);

[GenerateIdentity]
public class EntityWithByteIdMock2(EntityWithByteIdMock2Id id) : Entity<EntityWithByteIdMock2Id>(id);

[GenerateIdentity]
public record RecordWithId;

[GenerateIdentity]
// ReSharper disable once UnusedTypeParameter
public record RecordWithGenericType<T>;

[GenerateIdentity]
public class ClassWithId;

[GenerateIdentity]
// ReSharper disable once UnusedTypeParameter
public class ClassWithGenericType<T>;

[GenerateIdentity<Uuid>]
public class ClassWithUuid;

[GenerateIdentity<GeneratedValueObjectId>]
public class ClassWithGeneratedValueObjectId;

[GenerateValueObject<int>(minimumValue: 9, maximumValue: Int32.MaxValue, useValidationExceptions: false)]
public readonly partial record struct GeneratedValueObjectId;
