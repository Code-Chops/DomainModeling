namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;

public sealed record IndexOutOfRangeException<TCollection>(string? CustomMessage = null) 
	: SystemException<IndexOutOfRangeException<TCollection>, object>(CustomMessage ?? $"Index out of range in {typeof(TCollection).Name}.");
