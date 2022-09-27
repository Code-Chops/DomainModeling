using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;

public sealed record NullValidationSystemException<TParameter>(string? CustomMessage = null) 
	: SystemException<NullValidationSystemException<TParameter>, TParameter>(CustomMessage ?? $"Required data for {typeof(TParameter).Name} is missing.");