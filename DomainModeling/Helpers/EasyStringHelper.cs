using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Helpers;

public static class EasyStringHelper
{
	public static string ToDisplayString(Type domainObjectType, object? parameters = null, string? extraText = null)
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
		
		return $"{domainObjectType.Name} {extraText}{parametersText}";
	}

	public static string ToExceptionString(Type exceptionType, string errorMessage, object? parameters = null, string? argumentText = null)
	{
		var extraInfo = argumentText is null ? null : $" argument: {argumentText}.";
		var parametersText = parameters is null ? null : $"Info: {ToDisplayString(exceptionType, parameters, extraInfo)}.";
		var message = $"{errorMessage}.{parametersText}";
		return message;
	}
}