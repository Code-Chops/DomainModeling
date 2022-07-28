using System.Collections;
using System.Diagnostics.CodeAnalysis;
using CodeChops.DomainDrivenDesign.DomainModeling.Helpers;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public abstract record ReadableEntityDictionary<TId, TEntity> : ValueObject, IReadOnlyDictionary<TId, TEntity>
	where TId : Id
	where TEntity : Entity
{
	// ReSharper disable once MemberCanBePrivate.Global
	protected Dictionary<TId, TEntity> Dictionary { get; } = new();

	public TEntity this[TId id] => this.Dictionary.GetValueOrDefault(id) ?? throw ExceptionHelpers.KeyNotFoundException<ReadableEntityDictionary<TEntity>, TId>(id);
	public IEnumerable<TId> Keys => this.Dictionary.Keys;
	public IEnumerable<TEntity> Values => this.Dictionary.Values;
	public int Count => this.Dictionary.Count;
	public bool ContainsKey(TId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<TId, TEntity>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(TId key, [MaybeNullWhen(false)] out TEntity value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public abstract record ReadableEntityDictionary<TEntity> : ValueObject, IReadOnlyDictionary<Id, TEntity>
	where TEntity : Entity
{
	// ReSharper disable once MemberCanBePrivate.Global
	protected Dictionary<Id, TEntity> Dictionary { get; } = new();

	public TEntity this[Id id] => this.Dictionary.GetValueOrDefault(id) ?? throw ExceptionHelpers.KeyNotFoundException<ReadableEntityDictionary<TEntity>, Id>(id);
	public IEnumerable<Id> Keys => this.Dictionary.Keys;
	public IEnumerable<TEntity> Values => this.Dictionary.Values;
	public int Count => this.Dictionary.Count;
	public bool ContainsKey(Id key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<Id, TEntity>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(Id key, [MaybeNullWhen(false)] out TEntity value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}