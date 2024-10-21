namespace CodeChops.DomainModeling.Identities;

public interface IId<TSelf, out TUnderlying> : IId<TUnderlying>, IComparable<TSelf>, IHasDefault<TSelf>
	where TSelf : IId<TSelf, TUnderlying>
	where TUnderlying : IEquatable<TUnderlying>?, IComparable<TUnderlying>?;

public interface IId<out TUnderlying> : IId
	where TUnderlying : IEquatable<TUnderlying>?, IComparable<TUnderlying>?
{
	TUnderlying? Value { get; }
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }
	object GetValue();
}
