﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Diagnostics;
using CodeChops.SourceGeneration.Utilities;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.PrimitiveValueObjectsGenerator;

[Generator]
public class PrimitiveValueObjectGenerator : IIncrementalGenerator
{
	internal const string AttributeNamespace	= "CodeChops.DomainDrivenDesign.DomainModeling.Attributes";
	internal const string StringAttributeName	= "GenerateStringValueObject";
	internal const string IntegralAttributeName	= "GenerateIntegralValueObject";
	
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{		
		var valueProvider = context.SyntaxProvider
			.CreateSyntaxProvider(
				predicate: PrimitiveValueSyntaxReceiver.CheckIfProbablyIsPrimitiveValueObject,
				transform: static (context, _) => PrimitiveValueSyntaxReceiver.GetModel((TypeDeclarationSyntax)context.Node, context.SemanticModel))
			.Where(static model => model is not null)
			.Collect()
			.Combine(context.AnalyzerConfigOptionsProvider);
		
		context.RegisterSourceOutput(source: valueProvider, action: static (context, valueProvider) => CreateSource(context, valueProvider.Left!, valueProvider.Right));
	}
	
	private static void CreateSource(SourceProductionContext context, ImmutableArray<ValueObject> models, AnalyzerConfigOptionsProvider configOptionsProvider)
	{
		foreach (var model in models)
		{
			var code = CreateSource(model);

			var fileName = model.Namespace is null 
				? model.Name 
				: $"{model.Namespace}.{model.Name}";
			
			fileName = FileNameHelpers.GetFileName(fileName, configOptionsProvider);
			
			context.AddSource(fileName, SourceText.From(code, Encoding.UTF8));
		}
	}

	private static string CreateSource(ValueObject data)
	{
		var integralObject = data as IntegralValueObject;
		var stringObject = data as StringValueObject;
		
		var code = $@"// <auto-generated />
#nullable enable
using System;
using System.Globalization;
using System.Text.RegularExpressions;

{GetNamespaceDeclaration()}

/// <summary>
/// {GetComments()}
/// </summary>
{data.Declaration} : IValueObject, IComparable<{data.Name}>{GetInterfaces()}
{{
	public override string ToString() => this.ToEasyString(new {{ Value = this._value }}, extraText: ""{data.PrimitiveTypeName}"");
	
	{GetComparison()}
	
	{GetEmptyInstance()}
	
	private readonly {data.PrimitiveTypeName} _value;
	
	{GetLength()}
	
	public static implicit operator {data.PrimitiveTypeName}({data.Name} obj) => obj._value;
	public static explicit operator {data.Name}({data.PrimitiveTypeName} value) => new(value);
	
	private {data.Name}({data.PrimitiveTypeName} value)
	{{
{GetIntegralValidation() ?? GetStringValidation()}
		this._value = value;
	}}

	[Obsolete(Error)]
	public {data.Name}() => throw new InvalidOperationException(Error);
	private const string Error = $""A value should be provided when initializing {data.Name}.""; 
	{GetEnumerator()}
}}

#nullable restore
";

		return code;
		

		string? GetNamespaceDeclaration() => data.Namespace is null 
			? null 
			: $@"namespace {data.Namespace};";

		string GetComments() => integralObject is not null 
			? $@"A number of type {integralObject.PrimitiveTypeName}." 
			: $@"An {stringObject!.StringCaseConversion} {stringObject.StringFormat} string.";

		string? GetInterfaces()
		{
			if (stringObject is null) return null;
			
			var interfaces = new StringBuilder();
			if (stringObject.GenerateEnumerable) interfaces.Append(", IEnumerable<char>");
			if (stringObject.GenerateEmptyStatic) interfaces.Append($", IHasEmptyInstance<{data.Name}>");
			
			return interfaces.ToString();
		}

		string GetComparison()
		{
			var comparison = stringObject is null
				? null 
				: $", StringComparison.{stringObject.CompareOptions}";
			
			return @$"
	public bool Equals({data.Name}? other) 
		=> {data.PrimitiveTypeName}.Equals(this._value, other?._value{comparison});
	
	public int CompareTo({data.Name} other) 
		=> {data.PrimitiveTypeName}.Compare(this._value, other._value{comparison});";
		}

		string? GetEmptyInstance() => stringObject?.GenerateEmptyStatic == true
			? $"public static {data.Name} Empty {{ get; }} = new();"
			: null;

		string? GetLength() => stringObject is not null 
			? "public int Length => this._value.Length;" 
			: null;

		string? GetStringValidation()
		{
			if (stringObject is null) return null;
			
			var validation = new StringBuilder();

			if (stringObject.StringFormat is not StringFormat.Default)
			{
				var formatRegex = stringObject.StringFormat switch
				{
					StringFormat.Alpha						=> "^[a-zA-Z]+$",
					StringFormat.AlphaWithUnderscore		=> "^[a-zA-Z_]+$",
					StringFormat.AlphaNumeric				=> "^[a-zA-Z0-9]+$",
					StringFormat.AlphaNumericWithUnderscore => "^[a-zA-Z0-9_]+$",
					_										=> throw new ArgumentOutOfRangeException(nameof(stringObject.StringFormat), stringObject.StringFormat, null)
				};

				validation.AppendLine($@"		if (Regex.IsMatch(value, ""{formatRegex}"", RegexOptions.Compiled)) throw new ArgumentException(""Invalid characters in {data.Name} of format {stringObject.StringFormat}. Value '{{value}}'."");");
			}
			
			if (stringObject.MinimumLength is not null)
				validation.AppendLine($@"		if (value.Length < {stringObject.MinimumLength}) throw new ArgumentException($""String {data.Name} is shorter ({{value.Length}}) than {nameof(stringObject.MinimumLength)} {stringObject.MinimumLength}."");");
			
			if (stringObject.MaximumLength is not null)
				validation.AppendLine($@"		if (value.Length > {stringObject.MaximumLength}) throw new ArgumentException($""String {data.Name} is longer ({{value.Length}}) than {nameof(stringObject.MaximumLength)} {stringObject.MaximumLength}."");");

			if (stringObject.StringCaseConversion is not StringCaseConversion.NoConversion)
				validation.AppendLine($"		value = value.To{stringObject.StringCaseConversion}();");

			return validation.ToString();
		}

		string? GetIntegralValidation()
		{
			if (integralObject is null) return null;

			var validation = new StringBuilder();

			if (integralObject.MinimumValue is not null)
				validation.AppendLine($@"		if (value < {integralObject.MinimumValue}) throw new ArgumentException($""{data.PrimitiveTypeName} {data.Name} is smaller ({{value}}) than {nameof(integralObject.MinimumValue)} {integralObject.MinimumValue}."");");
			
			if (integralObject.MaximumValue is not null)
				validation.AppendLine($@"		if (value > {integralObject.MaximumValue}) throw new ArgumentException($""{data.PrimitiveTypeName} {data.Name} is higher ({{value}}) than {nameof(integralObject.MaximumValue)} {integralObject.MaximumValue}."");");

			return validation.ToString();
		}

		string? GetEnumerator() => stringObject?.GenerateEnumerable == true
			? @"
	public IEnumerator<char> GetEnumerator() => this._value.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator()  => this.GetEnumerator();"
			: null;
		}
}
