﻿using CodeChops.DomainModeling.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChops.DomainModeling;

public static class RegistrationExtensions
{
	public static IServiceCollection AddIdentityJsonSerialization(this IServiceCollection services)
	{
		JsonSerialization.DefaultOptions.Converters.Add(new ValueObjectJsonConverterFactory());

		return services;
	}

	public static IServiceCollection AddValueTupleJsonSerialization(this IServiceCollection services)
	{
		JsonSerialization.DefaultOptions.Converters.Add(new ValueTupleJsonConverterFactory());

		return services;
	}
}
