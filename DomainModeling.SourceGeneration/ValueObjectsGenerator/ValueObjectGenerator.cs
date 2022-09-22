﻿using System.Collections.Immutable;
using CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;
using CodeChops.SourceGeneration.Utilities;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator;

[Generator]
public class ValueObjectGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{		
		var valueProvider = context.SyntaxProvider
			.CreateSyntaxProvider(
				predicate: ValueObjectSyntaxReceiver.CheckIfProbablyIsValueObjectToGenerate,
				transform: static (context, _) => ValueObjectSyntaxReceiver.GetModel((TypeDeclarationSyntax)context.Node, context.SemanticModel))
			.Where(static model => model is not null)
			.Collect()
			.Combine(context.AnalyzerConfigOptionsProvider);
		
		context.RegisterSourceOutput(source: valueProvider, action: static (context, valueProvider) => CreateSource(context, valueProvider.Left!, valueProvider.Right));
	}
	
	private static void CreateSource(SourceProductionContext context, ImmutableArray<ValueObjectBase> models, AnalyzerConfigOptionsProvider configOptionsProvider)
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

	private static ImmutableHashSet<string> Namespaces { get; } = new HashSet<string>(new[]
	{
		"System",
		"System.Collections",
		"System.Collections.Immutable",
		"System.Diagnostics.CodeAnalysis",
		"System.Globalization",
		"System.Text.RegularExpressions",
		"CodeChops.DomainDrivenDesign.DomainModeling.Exceptions",
		"CodeChops.DomainDrivenDesign.DomainModeling.Validation",
	}).ToImmutableHashSet();
	
	private static string CreateSource(ValueObjectBase data)
	{
		var code = $@"// <auto-generated />
#nullable enable
#pragma warning disable CS0612

{GetUsings()}

{GetNamespaceDeclaration()}

/// <summary>
/// {data.GetCommentsCode()}
/// </summary>
{data.ValueObjectType.GetObjectDeclaration()} {data.ValueObjectType.GetTypeNameWithGenericParameters()} : IValueObject{GetInterfaces()}
{data.TypeDeclarationSyntax.GetClassGenericConstraints()}
{{
	{GetToString()}
	
	{GetHashCode()}

	{GetEquals()}

	{GetComparison()}
	
	{GetEmptyStatic()}
	
	{GetProperty()}
	
	{data.GetLengthOrCountCode()}
	
	{GetCastCode()}

	{GetDefaultConstructor()}

	{GetParameterlessConstructor()}	

	{GetEnumerator()}

	{data.GetExtraCode()}
}}

#pragma warning restore CS0612
#nullable restore
";

		return code;
		
		
		string GetUsings()
		{
			var namespaces = Namespaces.Union(data.GetNamespaces());
			var namespaceUsings = namespaces
				.OrderBy(ns => ns.StartsWith("CodeChops"))
				.ThenBy(ns => ns)
				.Aggregate(new StringBuilder(), (sb, ns) => sb.AppendLine($"using {ns};"))
				.ToString();

			return namespaceUsings;
		}

		string? GetNamespaceDeclaration() => data.Namespace is null 
			? null 
			: $@"namespace {data.Namespace};";

		
		string GetInterfaces()
		{
			var interfaces = new StringBuilder();
			if (data.GenerateComparison && data.GetCompareToCode() is not null) interfaces.Append($", IEquatable<{data.Name}>");
			if (data.AddCustomValidation) interfaces.Append($", IHasCustomValidation");
			if (data.GenerateEmptyStatic) interfaces.Append($", IHasEmptyInstance<{data.Name}>");
			if (data.AddIComparable && data.GenerateComparison) interfaces.Append($", IComparable<{data.Name}>");
			if (data.GenerateEnumerable && data is IEnumerableValueObject enumerableValueObject) interfaces.Append($", IEnumerable<{enumerableValueObject.ElementTypeName}>");

			var extraInterfaces = data.GetInterfacesCode();
			if (extraInterfaces is not null) interfaces.Append($", {extraInterfaces}");
			
			return interfaces.ToString();
		}
		
		
		string? GetToString() => data.GenerateToString ? data.GetToStringCode() : null;

		
		string? GetHashCode() => data.GenerateComparison
			? data.GetHashCodeCode()
			: null;
		
		
		string? GetEquals()
		{
			if (!data.GenerateComparison) return null;
			
			return $@"
	{data.GetEqualsCode()}
	{(data.ValueObjectType.IsRecord ? null : data.GetObjectEqualsCode())}";
		}


		string? GetComparison()
		{
			var equalityOperators = data.ValueObjectType.IsRecord
				? null
				: $@"
	public static bool operator ==({data.Name} left, {data.Name} right) => left.{data.PropertyName} == right.{data.PropertyName};
	public static bool operator !=({data.Name} left, {data.Name} right) => !(left == right);";
			
			if (!data.GenerateComparison) return equalityOperators;
			
			var compareToCode = data.GetCompareToCode();
			if (compareToCode is null) return equalityOperators;
			
			return $@"
	{equalityOperators}
	{compareToCode}

	public static bool operator <	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) <	0;
	public static bool operator <=	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) <= 0;
	public static bool operator >	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) >	0;
	public static bool operator >=	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) >= 0;
";
		}
		
		
		string? GetEmptyStatic()
		{
			if (!data.GenerateEmptyStatic) return null;
			
			return data.GenerateParameterlessConstructor 
				? $"public static {data.Name} Empty {{ get; }} = new();"
				: $"public static {data.Name} Empty {{ get; }} = new({data.GetDefaultValue()});";
		}


		string GetProperty()
		{
			var callValidation = data.AddCustomValidation ? "this.Validate();" : null;
			var error = $"Don't use this field, use the {data.PropertyName} property instead";

			return $@"	
#pragma warning disable CS0618 
	/// <summary>
    /// The primitive structural value.
    /// </summary>
	{(data.PropertyIsPublic ? "public" : "private")} {data.UnderlyingTypeName} {data.PropertyName} 
	{{
		get => this.{data.BackingFieldName};
		{(data.PropertyIsPublic ? "private " : null)}init 
		{{ 
{data.GetValidationCode()}
			this.{data.BackingFieldName} = value;

			{callValidation}
		}}
	}}
#pragma warning restore CS0618 

    /// <summary>
    /// Backing field for the structural value. {error} <see cref='{data.PropertyName}'/>.
	/// </summary>
	[Obsolete(""{error}."")]
	private readonly {data.UnderlyingTypeName} {data.BackingFieldName} = {data.GetDefaultValue()}!;";
		}


		string GetCastCode()
		{
			var code = new StringBuilder();
			code.AppendLine($@"public static implicit operator {data.UnderlyingTypeName}({data.Name} {data.LocalVariableName}) => {data.LocalVariableName}.{data.PropertyName};");
			
			if (!data.GenerateDefaultConstructor) return code.ToString();
			
			code.AppendLine($"	public static explicit operator {data.Name}({data.UnderlyingTypeName} {data.LocalVariableName}) => new({data.LocalVariableName});");
			
			var extraCastCode = data.GetExtraCastCode(); 
			if (extraCastCode is not null) code.AppendLine(extraCastCode);

			return code.ToString();
		}
		
		
		string? GetDefaultConstructor() => data.GenerateDefaultConstructor
			? $@"
	public {data.ValueObjectType.Name}({data.UnderlyingTypeName} {data.LocalVariableName})
	{{	
		this.{data.PropertyName} = {data.LocalVariableName};
	}}"
			: null;

		
		string GetParameterlessConstructor()
		{
			var error = $"Don't use this empty constructor. A value should be provided when initializing {data.Name}.";
			
			return data.GenerateParameterlessConstructor
				? $@"
	public {data.ValueObjectType.Name}()
	{{
		this.{data.PropertyName} = {data.GetDefaultValue()};
	}}"
				: $@"
#pragma warning disable CS8618
	[Obsolete(""{error}"", true)]
	public {data.ValueObjectType.Name}() => throw new InvalidOperationException($""{error}"");
#pragma warning restore CS8618";
		}
		

		string? GetEnumerator()
		{
			if (!data.GenerateEnumerable) return null;
			
			var enumeratorCode = data.GetEnumeratorCode();
			
			return enumeratorCode is null
				? null
				: @$"
	{enumeratorCode}
	IEnumerator IEnumerable.GetEnumerator()  => this.GetEnumerator();";
		}
	}
}
