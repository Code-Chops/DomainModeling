namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.Identities;

[GenerateIdentity<byte>]
public partial class EntityWithByteIdMock1 : Entity<EntityWithByteIdMock1.Identity>
{
	public EntityWithByteIdMock1(Identity id) 
		: base(id)
	{
	}
}

[GenerateIdentity]
public partial class EntityWithByteIdMock2 : Entity<EntityWithByteIdMock2.Identity>
{
	public EntityWithByteIdMock2(Identity id) 
		: base(id)
	{
	}
}

[GenerateIdentity]
public partial record RecordWithId;

[GenerateIdentity]
// ReSharper disable once UnusedTypeParameter
public partial record RecordWithGenericType<T>;

[GenerateIdentity]
public partial class ClassWithId
{
}

[GenerateIdentity]
// ReSharper disable once UnusedTypeParameter
public partial class ClassWithGenericType<T>
{
}

[GenerateIdentity<Uuid>]
public partial class ClassWithUuid
{
}

[GenerateIdentity<GeneratedValueObjectId>]
public partial class ClassWithGeneratedValueObjectId
{
}

public class EntityWithGeneratedId
{
	[GenerateValueObject<int>(minimumValue: 9, maximumValue: Int32.MaxValue, useValidationExceptions: false)]
	public readonly partial record struct GeneratedValueObjectId;
}

