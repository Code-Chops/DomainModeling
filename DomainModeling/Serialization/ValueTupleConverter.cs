using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

public class ValueTupleConverter<T1> : JsonConverter<ValueTuple<T1>>
{
	public override ValueTuple<T1> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (!reader.Read()) throw new JsonException();

		ValueTuple<T1> result = default;

		while (reader.TokenType != JsonTokenType.EndObject)
		{
			if (!reader.ValueTextEquals(typeof(T1).Name) && !reader.ValueTextEquals("Item1")) throw new JsonException();
			if (!reader.Read()) throw new JsonException();
			
			result.Item1 = JsonSerializer.Deserialize<T1>(ref reader, options) ?? throw new JsonException();

			reader.Read();
		}

		return result;
	}

	public override void Write(Utf8JsonWriter writer, ValueTuple<T1> value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(value.Item1?.GetType().Name ?? "Item1");
		JsonSerializer.Serialize(writer, value.Item1, options);
		writer.WriteEndObject();
	}
}

public class ValueTupleConverter<T1, T2> : JsonConverter<ValueTuple<T1, T2>>
{
	public override (T1, T2) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (!reader.Read()) throw new JsonException();
		
		(T1, T2) result = default;
		
		while (reader.TokenType != JsonTokenType.EndObject)
		{
			if (!reader.Read()) throw new JsonException();

			if (reader.ValueTextEquals(typeof(T1).Name) || reader.ValueTextEquals("Item1"))
				result.Item1 = JsonSerializer.Deserialize<T1>(ref reader, options) ?? throw new JsonException();
			else if (reader.ValueTextEquals(typeof(T2).Name) || reader.ValueTextEquals("Item2"))
				result.Item2 = JsonSerializer.Deserialize<T2>(ref reader, options) ?? throw new JsonException();
			else
				throw new JsonException();
					
			reader.Read();
		}

		return result;
	}

	public override void Write(Utf8JsonWriter writer, (T1, T2) value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(value.Item1?.GetType().Name ?? "Item1");
		JsonSerializer.Serialize(writer, value.Item1, options);
		writer.WritePropertyName(value.Item2?.GetType().Name ?? "Item2");
		JsonSerializer.Serialize(writer, value.Item2, options);
		writer.WriteEndObject();
	}
}

public class ValueTupleConverter<T1, T2, T3> : JsonConverter<ValueTuple<T1, T2, T3>>
{
	public override (T1, T2, T3) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (!reader.Read()) throw new JsonException();

		(T1, T2, T3) result = default;

		while (reader.TokenType != JsonTokenType.EndObject)
		{
			if (!reader.Read()) throw new JsonException();
			
			if (reader.ValueTextEquals(typeof(T1).Name) || reader.ValueTextEquals("Item1"))
				result.Item1 = JsonSerializer.Deserialize<T1>(ref reader, options) ?? throw new JsonException();
			else if (reader.ValueTextEquals(typeof(T2).Name) || reader.ValueTextEquals("Item2"))
				result.Item2 = JsonSerializer.Deserialize<T2>(ref reader, options) ?? throw new JsonException();
			else if (reader.ValueTextEquals(typeof(T3).Name) || reader.ValueTextEquals("Item3"))
				result.Item3 = JsonSerializer.Deserialize<T3>(ref reader, options) ?? throw new JsonException();
			else
				throw new JsonException();
			
			reader.Read();
		}

		return result;
	}

	public override void Write(Utf8JsonWriter writer, (T1, T2, T3) value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(value.Item1?.GetType().Name ?? "Item1");
		JsonSerializer.Serialize(writer, value.Item1, options);
		writer.WritePropertyName(value.Item2?.GetType().Name ?? "Item2");
		JsonSerializer.Serialize(writer, value.Item2, options);
		writer.WritePropertyName(value.Item3?.GetType().Name ?? "Item3");
		JsonSerializer.Serialize(writer, value.Item3, options);
		writer.WriteEndObject();
	}
}