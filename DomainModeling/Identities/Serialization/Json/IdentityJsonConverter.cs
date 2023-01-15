using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.DomainModeling.Identities.Serialization.Json;

internal sealed class IdentityJsonConverter<TId, TUnderlying> : JsonConverter<TId>
	where TId : IId<TUnderlying>
	where TUnderlying : IEquatable<TUnderlying?>, IComparable<TUnderlying?>
{
	public override bool CanConvert(Type typeToConvert) 
		=> typeToConvert.IsAssignableTo(typeof(TId));
	
	private static JsonConverter<TUnderlying> DefaultConverter { get; } = (JsonConverter<TUnderlying>)new JsonSerializerOptions().GetConverter(typeof(TUnderlying));
	
	public override TId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Check for null values
		if (reader.TokenType == JsonTokenType.Null) return default;
		
		if (reader.TokenType is not JsonTokenType.String and not JsonTokenType.Number) 
			throw new JsonException($"Unexpected token found in JSON: {reader.TokenType}. Expected: {JsonTokenType.String}.");

		var id = (TId)FormatterServices.GetUninitializedObject(typeof(TId));

		var type = typeof(TId).IsValueType ? typeof(TId) : typeof(TId).BaseType;

		if (type is null)
			throw new InvalidOperationException($"Expected type '{typeof(TId).Name}' to have a base type.");
		
		var propertyInfo = type.GetProperty(nameof(IId<TUnderlying>.Value), BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
			?? throw new InvalidOperationException($"Could not find settable property '{nameof(IId<TUnderlying>.Value)}' of {typeof(TId).Name}.");
		
		var underlyingValue = DefaultConverter.Read(ref reader, typeof(TUnderlying), options)!;
		
		propertyInfo.SetValue(id, underlyingValue, BindingFlags.Instance | BindingFlags.NonPublic, binder: null, index: null, CultureInfo.InvariantCulture);

		return id;
	}

	public override void Write(Utf8JsonWriter writer, TId id, JsonSerializerOptions options) 
		=> DefaultConverter.Write(writer, id.Value, options);
}
