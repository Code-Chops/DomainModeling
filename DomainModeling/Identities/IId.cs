namespace CodeChops.DomainModeling.Identities;

public interface IId<in TSelf, out TUnderlying> : IId<TSelf>
	where TSelf : IId<TSelf, TUnderlying>, new() 
	where TUnderlying : IEquatable<TUnderlying>?, IComparable<TUnderlying>?
{
	TUnderlying Value { get; }
}

public interface IId<in TSelf> : IId, IComparable<TSelf>
	where TSelf : IId<TSelf>, new()
{
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }
}
