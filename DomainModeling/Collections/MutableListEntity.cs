using CodeChops.DomainDrivenDesign.DomainModeling.Helpers;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableDomainObjectList<TDomainObject> : Entity, IReadOnlyList<TDomainObject>
	where TDomainObject : IDomainObject
{
	public override string ToString() => $"{this.GetType().Name} {{ {nameof(TDomainObject)} = {typeof(TDomainObject).Name} }}";
	
	protected abstract IReadOnlyList<TDomainObject> List { get; }
	
	public int Count => this.List.Count;
	public TDomainObject this[int index] => this.List.ElementAtOrDefault(index) ?? throw ExceptionHelpers.IndexOutOfRangeException<ImmutableDomainObjectList<TDomainObject>>(index);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<TDomainObject> GetEnumerator() => this.List.GetEnumerator();
}