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
		try
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
		
#pragma warning disable CS0168
		catch (Exception e)
#pragma warning restore CS0168
		{
			var descriptor = new DiagnosticDescriptor(nameof(ValueObjectGenerator), "Error", $"{nameof(ValueObjectGenerator)} failed to generate due to an error. Please inform CodeChops (www.CodeChops.nl). Error: {e}", "Compilation", DiagnosticSeverity.Error, isEnabledByDefault: true);
			context.ReportDiagnostic(Diagnostic.Create(descriptor, null));

			context.AddSource($"{nameof(ValueObjectGenerator)}_Exception_{Guid.NewGuid()}", SourceText.From($"/*{e}*/", Encoding.UTF8));
		}
	}

	private static ImmutableHashSet<string> Namespaces { get; } = new HashSet<string>(new[]
	{
		"System",
		"System.Collections",
		"System.Collections.Immutable",
		"System.ComponentModel",
		"System.Diagnostics.CodeAnalysis",
		"System.Globalization",
		"System.Runtime.InteropServices",
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
[StructLayout(LayoutKind.Auto)]
{data.ValueObjectType.GetObjectDeclaration()} {data.ValueObjectType.GetTypeNameWithGenericParameters()} : IValueObject{GetInterfaces()}
{data.TypeDeclarationSyntax.GetClassGenericConstraints()}
{{
{GetToString()}

{GetHashCode()}

{GetEquals()}

{GetComparison()}

{GetEmptyStatic()}

{GetProperty()}

{GetLengthOrCountCode()}

{GetCastCode()}

{GetDefaultConstructor()}

{AddForbiddenParameterlessConstructor()}	

{GetEnumerator()}

{GetExtraCode()}
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
			if (data.GenerateStaticDefault) interfaces.Append($", IHasDefaultInstance<{data.Name}>");
			if (data.AddIComparable && data.GenerateComparison) interfaces.Append($", IComparable<{data.Name}>");
			if (data.GenerateEnumerable && data is IEnumerableValueObject enumerableValueObject) interfaces.Append($", IEnumerable<{enumerableValueObject.ElementTypeName}>");

			var extraInterfaces = data.GetInterfacesCode();
			if (extraInterfaces is not null) interfaces.Append($", {extraInterfaces}");
			
			return interfaces.ToString();
		}
		
		
		string? GetToString() => data.GenerateToString 
			? $@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetToStringCode()}" 
			: null;

		
		string? GetHashCode() => data.GenerateComparison
			? $@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetHashCodeCode()}" 
			: null;


		string? GetEquals()
		{
			if (!data.GenerateComparison) return null;

			var equalsCode = new StringBuilder($@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetEqualsCode()}
");

			if (!data.ValueObjectType.IsRecord)
				equalsCode.Append($@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetObjectEqualsCode()}
");

			return equalsCode.ToString();
		}

		string? GetComparison()
		{
			var equalityOperators = data.ValueObjectType.IsRecord
				? null
				: $@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator ==({data.Name} left, {data.Name} right) => left.{data.PropertyName} == right.{data.PropertyName};
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator !=({data.Name} left, {data.Name} right) => !(left == right);";
			
			if (!data.GenerateComparison) return equalityOperators;
			
			var compareToCode = data.GetCompareToCode();
			if (compareToCode is null) return equalityOperators;
			
			return $@"
	{equalityOperators}
	[EditorBrowsable(EditorBrowsableState.Never)]
	{compareToCode}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) <	0;
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <=	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) <= 0;
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) >	0;
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >=	({data.Name} left, {data.Name} right)	=> left.CompareTo(right) >= 0;
";
		}
		
		
		string? GetEmptyStatic()
		{
			if (!data.GenerateStaticDefault) return null;
			
			return data.AddParameterlessConstructor 
				? $@"
	[EditorBrowsable(EditorBrowsableState.Never)]	
	public static {data.Name} DefaultInstance {{ get; }} = new()
;"
				: $@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static {data.Name} DefaultInstance {{ get; }} = new({data.GetDefaultValue()});
";
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
		[EditorBrowsable(EditorBrowsableState.Never)]
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
	[EditorBrowsable(EditorBrowsableState.Never)]
	private readonly {data.UnderlyingTypeName} {data.BackingFieldName} = {data.GetDefaultValue()}!;";
		}

		string? GetLengthOrCountCode()
		{
			var code = data.GetLengthOrCountCode();

			return code is null
				? null
				: $@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	{code}
";
		}
		
		
		string GetCastCode()
		{
			var code = new StringBuilder();
			code.AppendLine($@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static implicit operator {data.UnderlyingTypeName}({data.Name} {data.LocalVariableName}) => {data.LocalVariableName}.{data.PropertyName};");
			
			if (!data.GenerateDefaultConstructor) return code.ToString();
			
			code.AppendLine($@"	
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static explicit operator {data.Name}({data.UnderlyingTypeName} {data.LocalVariableName}) => new({data.LocalVariableName});");
			
			var extraCastCode = data.GetExtraCastCode(); 
			
			if (extraCastCode is not null) code.AppendLine($@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	{extraCastCode}");

			return code.ToString();
		}
		
		
		string? GetDefaultConstructor() => data.GenerateDefaultConstructor
			? $@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	public {data.ValueObjectType.Name}({data.UnderlyingTypeName} {data.LocalVariableName})
	{{	
		this.{data.PropertyName} = {data.LocalVariableName};
	}}"
			: null;

		
		string? AddForbiddenParameterlessConstructor()
		{
			if (data.AddParameterlessConstructor) return null;
			
			var error = $"Don't use this empty constructor. A value should be provided when initializing {data.Name}.";
			
			return $@"
#pragma warning disable CS8618
	[Obsolete(""{error}"", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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
	[EditorBrowsable(EditorBrowsableState.Never)]
	{enumeratorCode}
	[EditorBrowsable(EditorBrowsableState.Never)]
	IEnumerator IEnumerable.GetEnumerator()  => this.GetEnumerator();";
		}


		string? GetExtraCode()
		{
			var code = data.GetExtraCode();
			
			return code is null
				? null
				: $@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetExtraCode()}
";
		}
	}
}
