namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableListEntity<TDomainObject> : Entity, IReadOnlyList<TDomainObject>
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToEasyString(new { TDomainObject = typeof(TDomainObject).Name });
	
	protected abstract List<TDomainObject> List { get; }

	public int Count => this.List.Count;
	
	public virtual TDomainObject this[int index] 
		=> this.List.ElementAtOrDefault(index) ?? throw IndexOutOfRangeException<MutableListEntity<TDomainObject>>.Create(index);

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TDomainObject> GetEnumerator() => this.List.GetEnumerator();
}