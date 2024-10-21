using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.DomainModeling.Serialization.Json;

/// <summary>
/// Concerts identities to JSON and vice versa.
/// </summary>
public sealed class ValueObjectJsonConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
		=> !typeToConvert.IsAbstract && typeToConvert.IsAssignableTo(typeof(IValueObject));

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		const BindingFlags bindingFlags = BindingFlags.Instance
		                                  | BindingFlags.Public
		                                  | BindingFlags.NonPublic
		                                  | BindingFlags.GetProperty;

		var idUnderlying = typeToConvert.GetProperty("Value", bindingFlags)?.PropertyType
		                  ?? throw new InvalidOperationException($"Underlying type of {typeToConvert.Name} was not found.");

		var converter = Activator.CreateInstance(
			type: typeof(ValueObjectJsonConverter<,>).MakeGenericType(typeToConvert, idUnderlying),
			bindingAttr: BindingFlags.Instance | BindingFlags.Public,
			binder: null,
			args: null,
			culture: null);

		if (converter is null)
			throw new InvalidOperationException($"Could not create {typeof(ValueObjectJsonConverter<,>).Name} for type {typeToConvert.Name}.");

		return (JsonConverter)converter;
	}
}
