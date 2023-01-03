﻿using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Serialization.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization.Json;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

public static class JsonSerialization
{
	public static JsonSerializerOptions DefaultOptions { get; } = new()
	{
		WriteIndented = false, 
		Converters = { new ValueTupleJsonConverterFactory(), new IdentityJsonConverterFactory() },
	};
	
	public static JsonSerializerOptions DefaultDisplayStringOptions { get; } = new()
	{
		WriteIndented = false, 
		Converters = { new ValueTupleJsonConverterFactory(), new IdentityJsonConverterFactory() },
		IgnoreReadOnlyFields = false,
		DefaultIgnoreCondition = JsonIgnoreCondition.Never,
		MaxDepth = 3,
		ReferenceHandler = ReferenceHandler.IgnoreCycles,
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
	};
}
