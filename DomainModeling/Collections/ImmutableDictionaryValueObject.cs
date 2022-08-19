namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public record ImmutableDictionaryValueObject<TId, TDomainObject> : IValueObject, IReadOnlyDictionary<TId, TDomainObject>, IHasEmptyInstance<ImmutableDictionaryValueObject<TId, TDomainObject>> 
	where TId : IId where TDomainObject : IDomainObject
{
	public override string ToString() => this.ToEasyString(new { TId = typeof(TId).Name, TDomainObject = typeof(TDomainObject).Name });
	
	#region Comparison
	
	public override int GetHashCode()
		=> this.Count == 0
			? 1
			: 2;
	
	public virtual bool Equals(ImmutableDictionaryValueObject<TId, TDomainObject>? other)
	{
		if (ReferenceEquals(this, other)) return true;
		if (other is null) return false;
		if (!this.SequenceEqual(other)) return false;
		return true;
	}

	#endregion
	
	public static ImmutableDictionaryValueObject<TId, TDomainObject> Empty { get; } = new(new Dictionary<TId, TDomainObject>().ToImmutableDictionary());
	
	// ReSharper disable once MemberCanBePrivate.Global
	protected ImmutableDictionary<TId, TDomainObject> Dictionary { get; }

	public ImmutableDictionaryValueObject(ImmutableDictionary<TId, TDomainObject> dictionary)
	{
		this.Dictionary = dictionary;
	}

	public void Deconstruct(out ImmutableDictionary<TId, TDomainObject> dictionary)
	{
		dictionary = this.Dictionary;
	}
	
	public int Count => this.Dictionary.Count;
	public virtual TDomainObject this[TId id] 
		=> this.Dictionary.GetValueOrDefault(id) ?? throw DomainObjectKeyNotFoundException<TId, TDomainObject>.Create(id);
	public virtual TDomainObject this[TId id, [CallerMemberName] string? callerName = null] 
		=> this.Dictionary.GetValueOrDefault(id) ?? throw DomainObjectKeyNotFoundException<TId, TDomainObject>.Create(id, callerName);

	public IEnumerable<TId> Keys => this.Dictionary.Keys;
	public IEnumerable<TDomainObject> Values => this.Dictionary.Values;
	public bool ContainsKey(TId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<TId, TDomainObject>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(TId key, [MaybeNullWhen(false)] out TDomainObject value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}