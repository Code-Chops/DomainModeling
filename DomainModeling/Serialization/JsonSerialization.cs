using System.Text.Json;
using System.Text.Json.Serialization;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Serialization.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization.Json;
using CodeChops.GenericMath.Serialization.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

public static class JsonSerialization
{
	public static JsonSerializerOptions DefaultOptions { get; } = new()
	{
		WriteIndented = false, 
		Converters = { new ValueTupleJsonConverterFactory(), new IdentityJsonConverterFactory(), new NumberJsonConverterFactory() },
	};
	
	public static JsonSerializerOptions DefaultDisplayStringOptions { get; } = new()
	{
		WriteIndented = false, 
		Converters = { new ValueTupleJsonConverterFactory(), new IdentityJsonConverterFactory(), new NumberJsonConverterFactory()  },
		IgnoreReadOnlyFields = false,
		DefaultIgnoreCondition = JsonIgnoreCondition.Never,
		MaxDepth = 3,
		ReferenceHandler = ReferenceHandler.IgnoreCycles,
	};
}
