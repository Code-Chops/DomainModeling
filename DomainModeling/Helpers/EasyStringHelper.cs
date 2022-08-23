using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Helpers;

public static class EasyStringHelper
{
	public static string ToDisplayString<TObject>(object? parameters = null, string? extraText = null)
	{
		var parametersText = parameters is null 
			? null 
			: JsonSerializer.Serialize(parameters) // TODO rewrite to custom serializer.
				.Replace("\"", "")
				.Replace(":", " = ")
				.Replace(",", ", ")
				.Replace("{", "{ ")
				.Replace("}", " }");

		extraText = extraText is null ? null : $"({extraText}) ";
		
		return $"{typeof(TObject).Name} {extraText}{parametersText}";
	}
}