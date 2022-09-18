using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.DisplayString;

public static class DisplayStringHelper
{
	private static readonly JsonSerializerOptions SerializerOptions;

	static DisplayStringHelper()
	{
		SerializerOptions = new JsonSerializerOptions();
		SerializerOptions.Converters.Add(new ValueTupleFactory());
	}
	
	public static string ToDisplayString<TObject>(object? parameters = null, string? extraText = null)
	{
		var text = typeof(TObject) == typeof(object) ? "" : typeof(TObject).Name;
		text += extraText is null ? null : $" ({extraText}) ";
		
		text += parameters is null 
			? null 
			: JsonSerializer.Serialize(parameters, SerializerOptions) // TODO rewrite to custom serializer.
				.Replace("\"", "")
				.Replace(":", " = ")
				.Replace(",", ", ")
				.Replace("{", "{ ")
				.Replace("}", " }");
		
		return text;
	}
}