using System.Text.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Serialization.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

public static class JsonSerialization
{
	public static JsonSerializerOptions DefaultOptions { get; } = new()
	{
		WriteIndented = false, 
		Converters = { new ValueTupleJsonConverterFactory(), new IdentityJsonConverterFactory() }
	};
}