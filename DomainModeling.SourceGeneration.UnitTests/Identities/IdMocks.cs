namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.Identities;

[GenerateIdentity<byte>]
public partial class EntityWithByteIdMock1 : Entity
{
}

[GenerateIdentity]
public partial class EntityWithByteIdMock2 : Entity
{
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

