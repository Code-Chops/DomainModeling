using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using CodeChops.DomainDrivenDesign.DomainModeling.Helpers;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public abstract record ReadOnlyValueObjectDictionary<TId, TDomainObject>(ImmutableDictionary<TId, TDomainObject> Dictionary) : IValueObject, IReadOnlyDictionary<TId, TDomainObject>
	where TId : IId
	where TDomainObject : IDomainObject
{
	#region Comparison
	
	public override int GetHashCode()
		=> this.Count == 0
			? 1
			: 2;
	
	public virtual bool Equals(ReadOnlyValueObjectDictionary<TId, TDomainObject>? other)
	{
		if (ReferenceEquals(this, other)) return true;
		if (other is null) return false;

		foreach (var (key, value) in this)
			if (!other.TryGetValue(key, out var rightValue) || !EqualityComparer<TDomainObject>.Default.Equals(value, rightValue))
				return false;

		foreach (var (key, value) in this)
			if (!other.TryGetValue(key, out var leftValue) || !EqualityComparer<TDomainObject>.Default.Equals(value, leftValue))
				return false;

		return true;
	}

	#endregion
	
	
	// ReSharper disable once MemberCanBePrivate.Global
	protected ImmutableDictionary<TId, TDomainObject> Dictionary { get; } = Dictionary;

	public TDomainObject this[TId id] => this.Dictionary.GetValueOrDefault(id) ?? throw ExceptionHelpers.KeyNotFoundException<ReadOnlyValueObjectDictionary<TId, TDomainObject>, TId>(id);
	public IEnumerable<TId> Keys => this.Dictionary.Keys;
	public IEnumerable<TDomainObject> Values => this.Dictionary.Values;
	public int Count => this.Dictionary.Count;
	public bool ContainsKey(TId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<TId, TDomainObject>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(TId key, [MaybeNullWhen(false)] out TDomainObject value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}