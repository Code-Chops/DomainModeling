using System.Diagnostics.CodeAnalysis;
using CodeChops.DomainDrivenDesign.DomainModeling.Factories;
using CodeChops.DomainDrivenDesign.DomainModeling.Helpers;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public record ImmutableDomainObjectDictionary<TId, TDomainObject>(ImmutableDictionary<TId, TDomainObject> Dictionary) : IValueObject, IReadOnlyDictionary<TId, TDomainObject>, IHasEmptyInstance<ImmutableDomainObjectDictionary<TId, TDomainObject>>
	where TId : IId
	where TDomainObject : IDomainObject
{
	#region Comparison
	
	public override int GetHashCode()
		=> this.Count == 0
			? 1
			: 2;
	
	public virtual bool Equals(ImmutableDomainObjectDictionary<TId, TDomainObject>? other)
	{
		if (ReferenceEquals(this, other)) return true;
		if (other is null) return false;
		if (!this.SequenceEqual(other)) return false;
		return true;
	}

	#endregion
	
	public static ImmutableDomainObjectDictionary<TId, TDomainObject> Empty { get; } = new(new Dictionary<TId, TDomainObject>().ToImmutableDictionary());
	
	// ReSharper disable once MemberCanBePrivate.Global
	protected ImmutableDictionary<TId, TDomainObject> Dictionary { get; } = Dictionary;

	public int Count => this.Dictionary.Count;
	public TDomainObject this[TId id] => this.Dictionary.GetValueOrDefault(id) ?? throw ExceptionHelpers.KeyNotFoundException<ImmutableDomainObjectDictionary<TId, TDomainObject>, TId>(id);
	public IEnumerable<TId> Keys => this.Dictionary.Keys;
	public IEnumerable<TDomainObject> Values => this.Dictionary.Values;
	public bool ContainsKey(TId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<TId, TDomainObject>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(TId key, [MaybeNullWhen(false)] out TDomainObject value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}