using CodeChops.GenericMath.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public static class RegistrationExtensions
{
	public static IServiceCollection AddGenericNumberJsonSerialization(this IServiceCollection services)
	{
		JsonSerialization.DefaultOptions.Converters.Add(new NumberJsonConverterFactory());
		
		return services;
	}
}
