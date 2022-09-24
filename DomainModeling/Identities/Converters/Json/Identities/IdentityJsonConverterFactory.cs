﻿using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities.Converters.Json.Identities;

public class IdentityJsonConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) 
		=> !typeToConvert.IsAbstract && typeToConvert.IsAssignableTo(typeof(IId));

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var idPrimitive = typeToConvert.GetProperty(nameof(IId<int>.Value))?.PropertyType
		                  ?? throw new InvalidOperationException($"Primitive type of {typeToConvert.Name} was not found.");
		
		var converter = Activator.CreateInstance(
			type: typeof(IdentityJsonConverter<,>).MakeGenericType(typeToConvert, idPrimitive),
			bindingAttr: BindingFlags.Instance | BindingFlags.Public,
			binder: null,
			args: null,
			culture: null);

		if (converter is null) throw new InvalidOperationException($"Could not create {typeof(IdentityJsonConverter<,>).Name} for type {typeToConvert.Name}.");
		
		return (JsonConverter)converter;
	}
}