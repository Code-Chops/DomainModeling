namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public record ImmutableListValueObject<TDomainObject> : 
	IValueObject, 
	IReadOnlyList<TDomainObject>, 
	IHasEmptyInstance<ImmutableListValueObject<TDomainObject>>
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToEasyString(new { TDomainObject = typeof(TDomainObject).Name });
	
	#region Comparison

	public override int GetHashCode()
		=> this.Count == 0
			? 1
			: 2;

	public virtual bool Equals(ImmutableListValueObject<TDomainObject>? other)
	{
		if (ReferenceEquals(this, other)) return true;
		if (other is null) return false;
		return this.SequenceEqual(other);
	}

	#endregion
	
	public static ImmutableListValueObject<TDomainObject> Empty { get; } = new(new List<TDomainObject>().ToImmutableList());

	// ReSharper disable once MemberCanBePrivate.Global
	protected ImmutableList<TDomainObject> List { get; }

	public ImmutableListValueObject(ImmutableList<TDomainObject> list)
	{
		this.List = list;
	}

	public int Count => this.List.Count;
	
	public virtual TDomainObject this[int index] 
		=> this.List.ElementAtOrDefault(index) ?? throw IndexOutOfRangeException<ImmutableListValueObject<TDomainObject>>.Create(index);

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TDomainObject> GetEnumerator() => this.List.GetEnumerator();
}