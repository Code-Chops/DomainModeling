namespace CodeChops.DomainModeling.Identities;

public interface IId<in TSelf, out TUnderlying> : IId<TSelf>
	where TSelf : IId<TSelf, TUnderlying>
	where TUnderlying : IEquatable<TUnderlying>?, IComparable<TUnderlying>?
{
	protected static readonly TUnderlying DefaultValue = default!;
	
	TUnderlying Value { get; }
}

public interface IId<in TSelf> : IId, IComparable<TSelf>
	where TSelf : IId<TSelf>
{
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }

	object? GetValue();
}
