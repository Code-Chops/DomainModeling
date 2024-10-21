using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.DomainModeling.Serialization.Json;

internal sealed class ValueObjectJsonConverter<T, TUnderlying> : JsonConverter<T>
	where T : IValueObject
	where TUnderlying : IEquatable<TUnderlying?>, IComparable<TUnderlying?>
{
	public override bool CanConvert(Type typeToConvert)
		=> typeToConvert.IsAssignableTo(typeof(T));

	private static JsonConverter<TUnderlying> DefaultConverter { get; }
	private static MemberInfo MemberInfo { get; }

	static ValueObjectJsonConverter()
	{
		DefaultConverter = (JsonConverter<TUnderlying>)new JsonSerializerOptions().GetConverter(typeof(TUnderlying));
		MemberInfo = GetValueMember(typeof(T));
	}

	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Check for null values
		if (reader.TokenType is JsonTokenType.Null)
			return default;

		if (reader.TokenType is not (JsonTokenType.String or JsonTokenType.Number or JsonTokenType.True or JsonTokenType.False))
			throw new JsonException($"Unexpected token found in JSON: {reader.TokenType}.");

		var underlyingValue = DefaultConverter.Read(ref reader, typeof(TUnderlying), options)!;

		var id = (T)RuntimeHelpers.GetUninitializedObject(typeof(T));

		(MemberInfo as FieldInfo)?.SetValue(
			obj: id,
			value: underlyingValue,
			invokeAttr: BindingFlags.Instance | BindingFlags.NonPublic,
			binder: null,
			culture: CultureInfo.InvariantCulture);

		(MemberInfo as PropertyInfo)?.SetValue(
			obj: id,
			value: underlyingValue,
			index: null);

		return id;
	}

	public override void Write(Utf8JsonWriter writer, T valueObject, JsonSerializerOptions options)
	{
		var value = MemberInfo is PropertyInfo propertyInfo
			? (TUnderlying)propertyInfo.GetValue(valueObject)!
			: (TUnderlying)((FieldInfo)MemberInfo).GetValue(valueObject)!;

		DefaultConverter.Write(writer, value, options);
	}

	private static MemberInfo GetValueMember(Type type)
	{
		const BindingFlags bindingFlags = BindingFlags.Instance
		                                  | BindingFlags.Public
		                                  | BindingFlags.NonPublic;

		var member = (MemberInfo?)type.GetField(name: "_value", bindingAttr: bindingFlags | BindingFlags.SetField)
		               ?? type.GetProperty(name: "Value", bindingAttr: bindingFlags | BindingFlags.SetProperty);

		if (member is null)
			throw new InvalidOperationException($"Could not find value member of {typeof(T).Name}.");

		return member;
	}
}
