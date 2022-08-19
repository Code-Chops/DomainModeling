using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Extensions;

public static class DomainObjectExtensions
{
	public static string ToEasyString(this IDomainObject domainObject, object? parameters = null, string? extraText = null)
	{
		var parametersText = parameters is null 
			? null 
			: JsonSerializer.Serialize(parameters)
				.Replace("\"", "")
				.Replace(":", " = ")
				.Replace(",", ", ")
				.Replace("{", "{ ")
				.Replace("}", " }");

		extraText = extraText is null ? null : $"({extraText}) ";
		
		return $"{domainObject.GetType().Name} {extraText}{parametersText}";
	}
}