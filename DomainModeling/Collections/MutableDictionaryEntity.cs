namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public abstract class MutableDictionaryEntity<TId, TDomainObject> : Entity, IReadOnlyDictionary<TId, TDomainObject>
	where TId : IId
	where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToEasyString(new { TId = typeof(TId).Name, TDomainObject = typeof(TDomainObject).Name });
	
	protected abstract IReadOnlyDictionary<TId, TDomainObject> Dictionary { get; }
	
	public int Count => this.Dictionary.Count;

	public void Deconstruct(out IReadOnlyDictionary<TId, TDomainObject> dictionary)
	{
		dictionary = this.Dictionary;
	}

	public virtual TDomainObject this[TId id] 
		=> this.Dictionary.TryGetValue(id, out var value) ? value : throw DomainObjectKeyNotFoundException<TId, TDomainObject>.Create(id);

	public IEnumerable<TId> Keys => this.Dictionary.Keys;
	public IEnumerable<TDomainObject> Values => this.Dictionary.Values;
	public bool ContainsKey(TId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<TId, TDomainObject>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(TId key, [MaybeNullWhen(false)] out TDomainObject value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}