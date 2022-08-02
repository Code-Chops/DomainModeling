namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IId<out TValue> : IId 
	where TValue : IEquatable<TValue>, IComparable<TValue>
{
	TValue Value { get; }
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }
	object GetValue();
}