﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public record ImmutableDomainObjectList<TDomainObject>(ImmutableList<TDomainObject> List) : IValueObject, IReadOnlyList<TDomainObject>, IHasEmptyInstance<ImmutableDomainObjectList<TDomainObject>>
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToEasyString(new { TDomainObject = typeof(TDomainObject).Name });
	
	#region Comparison

	public override int GetHashCode()
		=> this.Count == 0
			? 1
			: 2;

	public virtual bool Equals(ImmutableDomainObjectList<TDomainObject>? other)
	{
		if (ReferenceEquals(this, other)) return true;
		if (other is null) return false;
		return this.SequenceEqual(other);
	}

	#endregion
	
	public static ImmutableDomainObjectList<TDomainObject> Empty { get; } = new(new List<TDomainObject>().ToImmutableList());

	// ReSharper disable once MemberCanBePrivate.Global
	protected ImmutableList<TDomainObject> List { get; } = List;

	public int Count => this.List.Count;
	public TDomainObject this[int index] => this.List.ElementAtOrDefault(index) ?? Exceptions.IndexOutOfRangeException<ImmutableDomainObjectList<TDomainObject>>.Throw(index);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TDomainObject> GetEnumerator() => this.List.GetEnumerator();
}