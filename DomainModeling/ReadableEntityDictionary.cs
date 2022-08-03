using System.Collections;
using System.Diagnostics.CodeAnalysis;
using CodeChops.DomainDrivenDesign.DomainModeling.Helpers;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public abstract class ReadableEntityDictionary<TId, TEntity> : Entity, IReadOnlyDictionary<TId, TEntity>
	where TId : IId
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

public abstract class ReadableEntityDictionary<TEntity> : Entity, IReadOnlyDictionary<IId, TEntity>
	where TEntity : Entity
{
	// ReSharper disable once MemberCanBePrivate.Global
	protected Dictionary<IId, TEntity> Dictionary { get; } = new();

	public TEntity this[IId id] => this.Dictionary.GetValueOrDefault(id) ?? throw ExceptionHelpers.KeyNotFoundException<ReadableEntityDictionary<TEntity>, IId>(id);
	public IEnumerable<IId> Keys => this.Dictionary.Keys;
	public IEnumerable<TEntity> Values => this.Dictionary.Values;
	public int Count => this.Dictionary.Count;
	public bool ContainsKey(IId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<IId, TEntity>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(IId key, [MaybeNullWhen(false)] out TEntity value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}