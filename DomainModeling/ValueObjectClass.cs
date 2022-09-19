namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// <para><b>Prefer to use a record (struct) instead of this class, because records have structural equality comparison built in.</b></para>
/// <inheritdoc cref="IValueObject"/>
/// </summary>
public abstract class ValueObjectClass<TSelf> : IValueObject
	where TSelf : ValueObjectClass<TSelf>
{
	/// <summary>
	/// Use <see cref="DisplayStringExtensions.ToDisplayString{TDomainObject}"/> for easy string display.
	/// </summary>
	public abstract override string ToString();
	
	public abstract bool Equals(TSelf? other);
	public abstract override int GetHashCode();
	
	public override bool Equals(object? obj)
	{
		if (obj is not TSelf valueObject) return false;
		return this.Equals(valueObject);
	}
	
	public static bool operator ==(ValueObjectClass<TSelf>? left, ValueObjectClass<TSelf>? right)
	{
		if (left is null && right is null) return true;
		if (left is null || right is null) return false;
		return left.Equals(right);
	}
	
	public static bool operator !=(ValueObjectClass<TSelf>? left, ValueObjectClass<TSelf>? right) 
		=> !(left == right);
}