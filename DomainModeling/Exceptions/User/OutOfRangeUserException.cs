using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User;

public record OutOfRangeUserException<TCollection, TValue>(TValue Value, string? CustomMessage = null) 
	: UserException<OutOfRangeUserException<TCollection, TValue>>(CustomMessage ?? $"{typeof(TValue).Name} ({Value}) out of range in {typeof(TCollection).Name}.");