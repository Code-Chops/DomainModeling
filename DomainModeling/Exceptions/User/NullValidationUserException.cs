namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User;

public sealed record NullValidationUserException<TParameter>(string? CustomMessage = null) 
	: UserException<NullValidationUserException<TParameter>>(CustomMessage ?? $"Required data for {typeof(TParameter).Name} is missing.");
