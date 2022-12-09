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
public partial record RecordWithId;

[GenerateStronglyTypedId]
// ReSharper disable once UnusedTypeParameter
public partial record RecordWithGenericType<T>;

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

[GenerateStronglyTypedId<GeneratedValueObjectId>]
public partial class ClassWithGeneratedValueObjectId
{
}

public class EntityWithGeneratedId
{
	[GenerateValueObject<int>(minimumValue: 9, maximumValue: Int32.MaxValue, useValidationExceptions: false)]
	public readonly partial record struct GeneratedValueObjectId;
}

