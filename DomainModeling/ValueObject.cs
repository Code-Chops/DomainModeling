namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// Prefer to use a record over a class (<see cref="ValueObject{TSelf}"/>), because records have equality checks built in. 
/// <inheritdoc cref="IValueObject"/>
/// </summary>
public abstract record ValueObject : IValueObject
{
	public override string ToString() => $"{this.GetType().Name} {{ HashCode = {this.GetHashCode()} }}";
}

/// <summary>
/// Prefer to use the non-generic <see cref="ValueObject"/> (record class) instead of this class, because records have equality checks built in.
/// <inheritdoc cref="IValueObject"/>
/// </summary>
public abstract class ValueObject<TSelf> : IValueObject
	where TSelf : ValueObject<TSelf>
{
	public override string ToString() => $"{this.GetType().Name} {{ HashCode = {this.GetHashCode()} }}";
	
	protected abstract bool Equals(TSelf Other);
	public abstract override int GetHashCode();
	
	public override bool Equals(object? obj)
	{
		if (obj is not TSelf valueObject) return false;
		if (ReferenceEquals(valueObject, null)) return false;
		return this.Equals(valueObject);
	}
	
	public static bool operator ==(ValueObject<TSelf>? left, ValueObject<TSelf>? right)
	{
		if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
		if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
		return left.Equals(right);
	}
	
	public static bool operator !=(ValueObject<TSelf>? left, ValueObject<TSelf>? right) 
		=> !(left == right);
}