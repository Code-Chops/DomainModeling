namespace CodeChops.DomainModeling.Collections;

/// <summary>
/// An abstract entity that holds a hash set and which provides a public readable api. The implementation can decide if this can be mutable or not.
/// </summary>
/// <typeparam name="TElement">The type of the elements in the set.</typeparam>
public abstract class HashSetEntity<TId, TElement> : Entity<TId>, IReadOnlySet<TElement>
	where TId : IId<TId>
	where TElement : IDomainObject 
{
	public override string ToString() => this.ToDisplayString(new { TDomainObject = typeof(TElement).Name });
	
	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlySet<TElement> HashSet { get; }

	public int Count => this.HashSet.Count;
	
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TElement> GetEnumerator() => this.HashSet.GetEnumerator();
	
	public bool Contains(TElement item) => this.HashSet.Contains(item);
	public bool IsProperSubsetOf(IEnumerable<TElement> other) => this.HashSet.IsProperSubsetOf(other);
	public bool IsProperSupersetOf(IEnumerable<TElement> other) => this.HashSet.IsProperSupersetOf(other);
	public bool IsSubsetOf(IEnumerable<TElement> other) => this.HashSet.IsSubsetOf(other);
	public bool IsSupersetOf(IEnumerable<TElement> other) => this.HashSet.IsSupersetOf(other);
	public bool Overlaps(IEnumerable<TElement> other) => this.HashSet.Overlaps(other);
	public bool SetEquals(IEnumerable<TElement> other) => this.HashSet.SetEquals(other);
}
