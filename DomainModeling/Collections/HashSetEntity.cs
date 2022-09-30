namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

/// <summary>
/// An abstract entity that holds a hash set and which provides a public readable api. The implementation can decide if this can be mutable or not.
/// </summary>
/// <typeparam name="T">The type of the elements in the set.</typeparam>
public abstract class HashSetEntity<T> : Entity, IReadOnlyCollection<T>
	where T : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TDomainObject = typeof(T).Name });
	
	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlySet<T> HashSet { get; }

	public int Count => this.HashSet.Count;
	
	public virtual T this[int index] 
		=> this.HashSet.ElementAtOrDefault(index) ?? new IndexOutOfRangeException<HashSetEntity<T>>().Throw<T>(index);

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<T> GetEnumerator() => this.HashSet.GetEnumerator();
}