namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

/// <summary>
/// An abstract entity that holds a list and which provides a public readable api. The implementation can decide if this can be mutable or not.
/// </summary>
/// <typeparam name="T">The type of the elements in the list.</typeparam>
public abstract class MutableListEntity<T> : Entity, IReadOnlyList<T>
	where T : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TDomainObject = typeof(T).Name });
	
	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlyList<T> List { get; }

	public int Count => this.List.Count;
	
	public virtual T this[int index] 
		=> this.List.ElementAtOrDefault(index) ?? new IndexOutOfRangeException<MutableListEntity<T>>().Throw<T>(index);

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<T> GetEnumerator() => this.List.GetEnumerator();
}