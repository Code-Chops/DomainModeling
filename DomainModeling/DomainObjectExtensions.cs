using System.Text;
using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public static class DisplayStringExtensions
{
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
		
		var serializedParameters = JsonSerializer.Serialize(parameters, jsonSerializerOptions ?? JsonSerialization.DefaultDisplayStringOptions) // TODO rewrite to custom serializer.
			.Replace("\"", "")
			.Replace(":", " = ")
			.Replace(",", ", ")
			.Replace("{", "{ ")
			.Replace("}", " }");
		
		text.Append(serializedParameters);

		return text;
	}
	
	// // ReSharper disable ExplicitCallerInfoArgument
	// public static Validation<TObject> Validate<TObject>(this TObject _, bool throwWhenInvalid = true, 
	// 	[CallerMemberName] string? callerMemberName = null, [CallerFilePath] string? callerFilePath = null, [CallerLineNumber] int? callerLineNumber = null)
	// 	where TObject : IDomainObject
	// 	=> new(throwWhenInvalid, callerMemberName, callerFilePath, callerLineNumber);
}
