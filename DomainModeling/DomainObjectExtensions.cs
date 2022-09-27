using System.Text;
using System.Text.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public static class DisplayStringExtensions
{
	private static readonly JsonSerializerOptions DefaultSerializerOptions = new() { Converters = { new ValueTupleFactory() }};
	
	public static string ToDisplayString<TDomainObject>(this TDomainObject o, object? parameters = null, string? extraText = null, JsonSerializerOptions? jsonSerializerOptions = null)
		where TDomainObject : IDomainObject
	{
		var text = new StringBuilder(typeof(TDomainObject) == typeof(object) ? "" : typeof(TDomainObject).Name);

		text.Append(GetParametersDisplayString(parameters ?? o, extraText, jsonSerializerOptions));
		
		return text.ToString();
	}

	internal static StringBuilder GetParametersDisplayString(object? parameters = null, string? extraText = null, JsonSerializerOptions? jsonSerializerOptions = null)
	{
		var text = new StringBuilder();
		text.Append(extraText is null ? " " : $" ({extraText}) ");
		
		text.Append(JsonSerializer.Serialize(parameters, jsonSerializerOptions ?? DefaultSerializerOptions)) // TODO rewrite to custom serializer.
			.Replace("\"", "")
			.Replace(":", " = ")
			.Replace(",", ", ")
			.Replace("{", "{ ")
			.Replace("}", " }");

		return text;
	}
}