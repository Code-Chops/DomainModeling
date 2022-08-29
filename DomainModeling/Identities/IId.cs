namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IId<TPrimitive> : IId, IComparable<Id<TPrimitive>>
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	TPrimitive Value { get; }
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }
	object GetValue();
}