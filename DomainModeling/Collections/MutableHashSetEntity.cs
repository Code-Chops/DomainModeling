namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableHashSetEntity<TDomainObject> : Entity, IReadOnlyCollection<TDomainObject>
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TDomainObject = typeof(TDomainObject).Name });
	
	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlySet<TDomainObject> HashSet { get; }

	public int Count => this.HashSet.Count;
	
	public virtual TDomainObject this[int index] 
		=> this.HashSet.ElementAtOrDefault(index) ?? new IndexOutOfRangeException<MutableHashSetEntity<TDomainObject>>().Throw<TDomainObject>(index);

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TDomainObject> GetEnumerator() => this.HashSet.GetEnumerator();
}