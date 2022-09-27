using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User;

public record NullValidationUserException<TParameter>(string? CustomMessage = null) 
	: UserException<NullValidationUserException<TParameter>>(CustomMessage ?? $"Required data for {typeof(TParameter).Name} is missing.");
