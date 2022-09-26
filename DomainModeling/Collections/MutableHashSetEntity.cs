namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableHashSetEntity<TDomainObject> : Entity, IReadOnlyCollection<TDomainObject>
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TDomainObject = typeof(TDomainObject).Name });
	
	protected abstract HashSet<TDomainObject> HashSet { get; }

	public int Count => this.HashSet.Count;
	
	public virtual TDomainObject this[int index] 
		=> this.HashSet.ElementAtOrDefault(index) ?? throw IndexOutOfRangeException<MutableHashSetEntity<TDomainObject>>.Create(index);

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TDomainObject> GetEnumerator() => this.HashSet.GetEnumerator();
}