using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public static class RegistrationExtensions
{
	public static IServiceCollection AddIdentitiesJsonSerialization(this IServiceCollection services)
	{
		JsonSerialization.DefaultOptions.Converters.Add(new IdentityJsonConverterFactory());

		return services;
	}
}