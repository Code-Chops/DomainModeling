using System.Diagnostics.CodeAnalysis;
using CodeChops.DomainDrivenDesign.DomainModeling.Helpers;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableDomainObjectDictionary<TId, TDomainObject> : Entity, IReadOnlyDictionary<TId, TDomainObject>
	where TId : IId
	where TDomainObject : IDomainObject
{
	public override string ToString() => $"{this.GetType().Name} {{ {nameof(TId)} = {typeof(TId).Name}, {nameof(TDomainObject)} = {typeof(TDomainObject).Name} }}";
	
	protected abstract IReadOnlyDictionary<TId, TDomainObject> Dictionary { get; }
	
	public TDomainObject this[TId id] => this.Dictionary.TryGetValue(id, out var value) 
		? value 
		: throw ExceptionHelpers.KeyNotFoundException<MutableDomainObjectDictionary<TId, TDomainObject>, TId>(id);
	public IEnumerable<TId> Keys => this.Dictionary.Keys;
	public IEnumerable<TDomainObject> Values => this.Dictionary.Values;
	public int Count => this.Dictionary.Count;
	public bool ContainsKey(TId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<TId, TDomainObject>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(TId key, [MaybeNullWhen(false)] out TDomainObject value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}