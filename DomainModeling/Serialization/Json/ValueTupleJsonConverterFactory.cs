using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.DomainModeling.Serialization.Json;

public sealed class ValueTupleJsonConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
	{
		var iTuple = typeToConvert.GetInterface("System.Runtime.CompilerServices.ITuple");
		return iTuple is not null;
	}

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var genericArguments = typeToConvert.GetGenericArguments();

		var converterType = genericArguments.Length switch
		{
			1 => typeof(ValueTupleJsonConverter<>).MakeGenericType(genericArguments),
			2 => typeof(ValueTupleJsonConverter<,>).MakeGenericType(genericArguments),
			3 => typeof(ValueTupleJsonConverter<,,>).MakeGenericType(genericArguments),
			// And add other cases as needed
			_ => throw new NotSupportedException(),
		};
		return (JsonConverter)(Activator.CreateInstance(converterType)
		                       ?? throw new InvalidOperationException($"Could not create an instance of {converterType.Name}"));
	}
}
