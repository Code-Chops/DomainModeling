using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableDictionaryEntity<TKey, TDomainObject> : Entity, IReadOnlyDictionary<TKey, TDomainObject>
	where TKey : notnull
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TId = typeof(TKey).Name, TDomainObject = typeof(TDomainObject).Name });
	
	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlyDictionary<TKey, TDomainObject> Dictionary { get; }
	
	public int Count => this.Dictionary.Count;

	public IEnumerable<TKey> Keys => this.Dictionary.Keys;
	public IEnumerable<TDomainObject> Values => this.Dictionary.Values;
	
	public bool ContainsKey(TKey key) => this.Dictionary.ContainsKey(key);
	public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TDomainObject value) => this.Dictionary.TryGetValue(key, out value);

	public virtual TDomainObject this[TKey key] 
		=> this.Dictionary.TryGetValue(key, out var value) 
			? value 
			: new KeyNotFoundException<TKey, TDomainObject>().Throw<TDomainObject>(key);

	public IEnumerator<KeyValuePair<TKey, TDomainObject>> GetEnumerator() => this.Dictionary.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}