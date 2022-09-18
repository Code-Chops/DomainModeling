using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Helpers;

public static class EasyStringHelper
{
	public static string ToDisplayString<TObject>(object? parameters = null, string? extraText = null)
	{
		var text = typeof(TObject) == typeof(object) ? "" : typeof(TObject).Name;
		text += extraText is null ? null : $"({extraText}) ";
		
		text += parameters is null 
			? null 
			: JsonSerializer.Serialize(parameters) // TODO rewrite to custom serializer.
				.Replace("\"", "")
				.Replace(":", " = ")
				.Replace(",", ", ")
				.Replace("{", "{ ")
				.Replace("}", " }");
		
		return text;
	}
}