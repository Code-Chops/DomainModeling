namespace CodeChops.DomainModeling.Collections;

/// <summary>
/// An abstract entity that holds a dictionary and which provides a public readable api. The implementation can decide if this can be mutable or not.
/// </summary>
/// <typeparam name="TKey">The type of the keys in the dictionary (which should be a domain object).</typeparam>
/// <typeparam name="TValue">The type of the values in the dictionary (which should be a domain object).</typeparam>
public abstract class DictionaryEntity<TSelf, TId, TKey, TValue>(TId id) : Entity<TId>(id), IReadOnlyDictionary<TKey, TValue>
	where TSelf : DictionaryEntity<TSelf, TId, TKey, TValue>
	where TId : IId, IHasDefault<TId>
	where TKey : IDomainObject
	where TValue : IDomainObject
{
	public override string ToString() => this.ToDisplayString(new { TId = typeof(TKey).Name, TDomainObject = typeof(TValue).Name });

	// Is readonly (i.e.: readable) in order to make covariance possible.
	protected abstract IReadOnlyDictionary<TKey, TValue> Dictionary { get; }

	public int Count => this.Dictionary.Count;

	public IEnumerable<TKey> Keys => this.Dictionary.Keys;
	public IEnumerable<TValue> Values => this.Dictionary.Values;

	public bool ContainsKey(TKey key) => this.Dictionary.ContainsKey(key);
	public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => this.Dictionary.TryGetValue(key, out value);

	public virtual TValue this[TKey key]
		=> Validator.Get<TSelf>.Default.GuardKeyExists(this.GetValueOrDefault, key, errorCode: null);

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.Dictionary.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
