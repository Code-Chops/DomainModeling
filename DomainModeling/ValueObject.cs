namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <inheritdoc cref="IValueObject"/>
public abstract record ValueObject<TSelf> : IValueObject
	where TSelf : ValueObject<TSelf>
{
	/// <summary>
	/// <inheritdoc cref="IDomainObject.ToString()"/>
	/// </summary>
	public abstract override string? ToString();
}
