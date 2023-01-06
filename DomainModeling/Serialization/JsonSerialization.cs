using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodeChops.DomainModeling.Identities.Serialization.Json;
using CodeChops.DomainModeling.Serialization.Json;

namespace CodeChops.DomainModeling.Serialization;

public static class JsonSerialization
{
	public static JsonSerializerOptions DefaultOptions { get; } = new()
	{
		WriteIndented = false, 
		Converters = { new ValueTupleJsonConverterFactory(), new IdentityJsonConverterFactory() },
	};
	
	public static JsonSerializerOptions DefaultDisplayStringOptions { get; } = new()
	{
		WriteIndented = false, 
		Converters = { new ValueTupleJsonConverterFactory(), new IdentityJsonConverterFactory() },
		IgnoreReadOnlyFields = false,
		DefaultIgnoreCondition = JsonIgnoreCondition.Never,
		MaxDepth = 3,
		ReferenceHandler = ReferenceHandler.IgnoreCycles,
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
	};
}
