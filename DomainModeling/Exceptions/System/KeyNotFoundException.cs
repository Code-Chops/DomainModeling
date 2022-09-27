using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;

public sealed record KeyNotFoundException<TKey, TCollection>(string? CustomMessage = null) 
	: SystemException<KeyNotFoundException<TKey, TCollection>, TKey>(CustomMessage ?? $"{typeof(TKey).Name} not found in {typeof(TCollection).Name}.");