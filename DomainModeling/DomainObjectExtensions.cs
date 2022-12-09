using System.Text;
using System.Text.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling;

public static class DisplayStringExtensions
{
	/// <summary>
	/// Creates a display string of the domain object by serializing it and changing the ':' to '='. 
	/// </summary>
	/// <param name="parameters">
	/// Provide an anonymous object to customize the string. If omitted, it will create a string of all fields and properties of the class/struct.
	/// <para>For example, to only show the ID of an entity, use: new { Id = this.Id }</para>
	/// </param>
	/// <param name="extraText">Extra text that is added after the serialized string in parenthesis.</param>
	/// <param name="jsonSerializerOptions">Provide to customize the string serialization.</param>
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
		
		var serializedParameters = JsonSerializer.Serialize(parameters, jsonSerializerOptions ?? JsonSerialization.DefaultDisplayStringOptions)
			.Replace("\"", "")
			.Replace(":", " = ")
			.Replace(",", ", ")
			.Replace("{", "{ ")
			.Replace("}", " }");
		
		text.Append(serializedParameters);

		return text;
	}
}
