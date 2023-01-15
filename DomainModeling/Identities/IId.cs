namespace CodeChops.DomainModeling.Identities;

public interface IId<in TSelf, out TUnderlying> : IId<TUnderlying>, IComparable<TSelf>
	where TSelf : IId<TSelf, TUnderlying>
	where TUnderlying : IEquatable<TUnderlying>?, IComparable<TUnderlying>?
{
}

public interface IId<out TUnderlying> : IId
	where TUnderlying : IEquatable<TUnderlying>?, IComparable<TUnderlying>?
{
	protected static readonly TUnderlying DefaultValue = default!;
	
	TUnderlying Value { get; }
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }

	object? GetValue();
}
