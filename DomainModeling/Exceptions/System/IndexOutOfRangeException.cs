using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;

public record IndexOutOfRangeException<TCollection>(string? CustomMessage = null) 
	: SystemException<IndexOutOfRangeException<TCollection>, object>(CustomMessage ?? $"Index out of range in {typeof(TCollection).Name}.")
{
}