namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableDomainObjectList<TDomainObject> : Entity, IReadOnlyList<TDomainObject>
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToEasyString(new { TDomainObject = typeof(TDomainObject).Name });
	
	protected abstract IReadOnlyList<TDomainObject> List { get; }
	
	public int Count => this.List.Count;
	public TDomainObject this[int index] => this.List.ElementAtOrDefault(index) ?? Exceptions.IndexOutOfRangeException<ImmutableDomainObjectList<TDomainObject>>.Throw(index);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TDomainObject> GetEnumerator() => this.List.GetEnumerator();
}