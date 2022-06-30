﻿using CodeChops.Identities;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public abstract record ReadableEntityDictionary<TId, TEntity> : ValueObject, IReadOnlyDictionary<TId, TEntity>
	where TId : IId
	where TEntity : IEntity<TId>
{
	protected Dictionary<TId, TEntity> Dictionary { get; set; } = new();

	public TEntity this[TId id] => this.Dictionary.GetValueOrDefault(id) ?? throw ExceptionHelpers.KeyNotFoundException<EntityDictionary<TEntity>, TId>(id);
	public IEnumerable<TId> Keys => this.Dictionary.Keys;
	public IEnumerable<TEntity> Values => this.Dictionary.Values;
	public int Count => this.Dictionary.Count;
	public bool ContainsKey(TId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<TId, TEntity>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(TId key, [MaybeNullWhen(false)] out TEntity value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public abstract record EntityDictionary<TEntity> : ValueObject, IReadOnlyDictionary<IId, TEntity>
	where TEntity : IEntity
{
	protected Dictionary<IId, TEntity> Dictionary { get; set; } = new();

	public TEntity this[IId id] => this.Dictionary.GetValueOrDefault(id) ?? throw ExceptionHelpers.KeyNotFoundException<EntityDictionary<TEntity>, IId>(id);
	public IEnumerable<IId> Keys => this.Dictionary.Keys;
	public IEnumerable<TEntity> Values => this.Dictionary.Values;
	public int Count => this.Dictionary.Count;
	public bool ContainsKey(IId key) => this.Dictionary.ContainsKey(key);
	public IEnumerator<KeyValuePair<IId, TEntity>> GetEnumerator() => this.Dictionary.GetEnumerator();
	public bool TryGetValue(IId key, [MaybeNullWhen(false)] out TEntity value) => this.Dictionary.TryGetValue(key, out value);
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}