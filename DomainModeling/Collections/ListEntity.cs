namespace CodeChops.DomainModeling.Collections;

/// <summary>
/// An abstract entity that holds a list and which provides a public readable api. The implementation can decide if this can be mutable or not.
/// </summary>
/// <typeparam name="TElement">The type of the elements in the list.</typeparam>
public abstract class ListEntity<TSelf, TElement> : Entity, IReadOnlyList<TElement>
	where TSelf : ListEntity<TSelf, TElement>
	where TElement : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TDomainObject = typeof(TElement).Name });
	
	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlyList<TElement> List { get; }

	public int Count => this.List.Count;
	
	public virtual TElement this[int index] 
		=> Validator.Get<TSelf>.Default.GuardIndexInRange(this.List, index, errorCode: null)!;

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TElement> GetEnumerator() => this.List.GetEnumerator();
}
