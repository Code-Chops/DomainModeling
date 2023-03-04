namespace CodeChops.DomainModeling.Identities;

public interface IId<TSelf, out TUnderlying> : IId<TSelf>
	where TSelf : IId<TSelf, TUnderlying>
	where TUnderlying : IEquatable<TUnderlying>?, IComparable<TUnderlying>?
{
	TUnderlying Value { get; }
}

public interface IId<TSelf> : IId, ICreatable<TSelf>, IComparable<TSelf>
	where TSelf : IId<TSelf>
{
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }
}
