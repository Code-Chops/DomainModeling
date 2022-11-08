namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

/// <summary>
/// An abstract entity that holds a hash set and which provides a public readable api. The implementation can decide if this can be mutable or not.
/// </summary>
/// <typeparam name="T">The type of the elements in the set.</typeparam>
public abstract class HashSetEntity<T> : Entity, IReadOnlySet<T>
	where T : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TDomainObject = typeof(T).Name });
	
	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlySet<T> HashSet { get; }

	public int Count => this.HashSet.Count;
	
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<T> GetEnumerator() => this.HashSet.GetEnumerator();
	
	public bool Contains(T item) => this.HashSet.Contains(item);
	public bool IsProperSubsetOf(IEnumerable<T> other) => this.HashSet.IsProperSubsetOf(other);
	public bool IsProperSupersetOf(IEnumerable<T> other) => this.HashSet.IsProperSupersetOf(other);
	public bool IsSubsetOf(IEnumerable<T> other) => this.HashSet.IsSubsetOf(other);
	public bool IsSupersetOf(IEnumerable<T> other) => this.HashSet.IsSupersetOf(other);
	public bool Overlaps(IEnumerable<T> other) => this.HashSet.Overlaps(other);
	public bool SetEquals(IEnumerable<T> other) => this.HashSet.SetEquals(other);
}
