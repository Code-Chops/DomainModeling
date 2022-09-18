using System.Text.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.DisplayString;

public static class DisplayStringHelper
{
	private static readonly JsonSerializerOptions DefaultSerializerOptions = new() { Converters = { new ValueTupleFactory() }};

	public static string ToDisplayString<TObject>(object? parameters = null, string? extraText = null, JsonSerializerOptions? jsonSerializerOptions = null!)
	{
		var text = typeof(TObject) == typeof(object) ? "" : typeof(TObject).Name;
		text += extraText is null ? null : $" ({extraText}) ";
		
		text += parameters is null 
			? null 
			: JsonSerializer.Serialize(parameters, jsonSerializerOptions ?? DefaultSerializerOptions) // TODO rewrite to custom serializer.
				.Replace("\"", "")
				.Replace(":", " = ")
				.Replace(",", ", ")
				.Replace("{", "{ ")
				.Replace("}", " }");
		
		return text;
	}
}