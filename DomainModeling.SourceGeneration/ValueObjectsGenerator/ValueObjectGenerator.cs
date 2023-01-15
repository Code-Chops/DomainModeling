using System.Collections.Immutable;
using CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;
using CodeChops.SourceGeneration.Utilities;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator;

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
				if (model.ErrorMessage is not null)
				{
					context.ReportDiagnostic(Diagnostic.Create(descriptor: new DiagnosticDescriptor(model.Name, "Error", $"Error generating value object '{model.Name}'. " + model.ErrorMessage, "Compilation", DiagnosticSeverity.Error, isEnabledByDefault: true), null));
					continue;
				}
				
				var code = CreateSource(model, configOptionsProvider);
	
				var fileName = model.Namespace is null 
					? model.Name 
					: $"{model.Namespace}.{model.Name}";
				
				fileName = FileNameHelpers.GetFileName(fileName, configOptionsProvider);
				
				context.AddSource(fileName, SourceText.From(code, Encoding.UTF8));
			}
		}
		
		catch (Exception e)
		{
			var descriptor = new DiagnosticDescriptor(nameof(ValueObjectGenerator), "Error", $"{nameof(ValueObjectGenerator)} failed to generate due to an error. Please inform CodeChops (www.CodeChops.nl). Error: {e}", "Compilation", DiagnosticSeverity.Error, isEnabledByDefault: true);
			context.ReportDiagnostic(Diagnostic.Create(descriptor, null));

			context.AddSource($"{nameof(ValueObjectGenerator)}_Exception_{Guid.NewGuid()}", SourceText.From($"/*{e}*/", Encoding.UTF8));
		}
	}

	private static string CreateSource(ValueObjectBase data, AnalyzerConfigOptionsProvider configOptionsProvider)
	{
		if (!configOptionsProvider.GlobalOptions.TryGetValue("build_property.RootNamespace", out var rootNamespace))
			rootNamespace = "global::";
		
		var code = new StringBuilder();
		
		code.AppendLine(@"// <auto-generated />
#nullable enable
#pragma warning disable CS0612 // Is deprecated (level 1)
#pragma warning disable CS0618 // Member is obsolete (level 2)
");

		code.AppendLine(GetUsings, trimEnd: true)
			.AppendLine()
			.AppendLine(GetNamespaceDeclaration, trimEnd: true)
			.AppendLine(GetComment, trimEnd: true)
			.AppendLine(GetStructLayoutAttribute, trimEnd: true)
			.AppendLine(GetObjectDeclaration, trimEnd: true)
			.AppendLine("{")
			.AppendLine(GetToString, trimEnd: true)
			.AppendLine(GetLengthOrCount, trimEnd: true)
			.AppendLine(GetProperty, trimEnd: true)
			.AppendLine(GetEqualsAndHashCode, trimEnd: true)
			.AppendLine(GetComparison, trimEnd: true)
			.AppendLine(GetStaticDefault, trimEnd: true)
			.AppendLine(GetCast, trimEnd: true)
			.AppendLine(GetEnumerator, trimEnd: true)
			.AppendLine(GetConstructors, trimEnd: true)
			.AppendLine(GetFactories, trimEnd: true)
			.AppendLine(GetExtraCode, trimEnd: true)
			.AppendLine("}")
			.Append(@"
#pragma warning restore CS0618 // Member is obsolete (level 2)
#pragma warning restore CS0612 // Is deprecated (level 1)
#nullable restore
");

		return code.ToString();
		
		
		string GetUsings()
		{
			var namespaces = data.GetNamespaces().Union(new[]
			{
				"System",
				"System.Collections",
				"System.Collections.Immutable",
				"System.ComponentModel",
				"System.Diagnostics",
				"System.Diagnostics.CodeAnalysis",
				"System.Globalization",
				"System.Runtime.InteropServices",
				"System.Text.RegularExpressions",
				"CodeChops.DomainModeling.Exceptions",
				"CodeChops.DomainModeling.Validation",
				rootNamespace,
			});
			
			var namespaceUsings = namespaces
				.Distinct()
				.OrderBy(ns => ns.StartsWith("CodeChops"))
				.ThenBy(ns => ns)
				.Aggregate(new StringBuilder(), (sb, ns) => sb.AppendLine($"using {ns};"))
				.ToString();

			return namespaceUsings;
		}
		

		string? GetNamespaceDeclaration() => data.Namespace is null 
			? null 
			: $@"namespace {data.Namespace};";


		string GetComment()
		{
			if (!String.IsNullOrWhiteSpace(data.ValueObjectType.GetDocumentationCommentXml()))
				return Environment.NewLine;
			
			return $@"
/// <summary>
/// {data.GetComments()}
/// </summary>
";
		}
		
		
		string? GetStructLayoutAttribute()
			=> data.ValueObjectType.TypeKind == TypeKind.Struct
				? "[StructLayout(LayoutKind.Auto)]"
				: null;

		
		string GetObjectDeclaration()
		{
			var code = new StringBuilder();

			var declaration = data.ValueObjectType.GetObjectDeclaration();
			
			if (data.ValueObjectType.TypeKind == TypeKind.Struct)
				declaration = declaration.Replace("partial", "readonly partial");

			code.AppendLine($"{declaration} {data.ValueObjectType.GetTypeNameWithGenericParameters()}{GetInterfaces()}");

			return code.ToString();
		}


		string? GetInterfaces()
		{
			if (data.ValueObjectType.IsRefLikeType) 
				return null;
			
			var interfaces = new StringBuilder($" : IValueObject, ICreatable<{data.Name}, {data.UnderlyingTypeName}>");
			if (data.GenerateComparison && data.GetCompareToCode() is not null) interfaces.Append($", IEquatable<{data.Name}{data.NullOperator}>");
			if (data.GenerateStaticDefault) interfaces.Append($", IHasDefault<{data.Name}>");
			if (data.AddIComparable && data.GenerateComparison) interfaces.Append($", IComparable<{data.Name}>");
			if (data.GenerateEnumerable && data is IEnumerableValueObject enumerableValueObject) interfaces.Append($", IEnumerable<{enumerableValueObject.ElementTypeName}>");
			if (data is StringValueObject { UseRegex: true }) interfaces.Append($", IHasValidationRegex");
			
			var extraInterfaces = data.GetInterfacesCode();
			if (extraInterfaces is not null) interfaces.Append($", {extraInterfaces}");
			
			return interfaces.ToString();
		}
		
		
		string GetToString() => data.GenerateToString 
			? $@"	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetToStringCode()}" 
			: $@"	public override partial string ToString();";


		string? GetLengthOrCount()
		{
			var code = data.GetLengthOrCountCode();

			return code is null
				? null
				: $@"
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{code}";
		}


		string GetProperty()
		{
			var error = data.GenerateDefaultConstructor 
				? $"Don't use this field, use the {data.PropertyName} property instead."
				: null;

			var code = new StringBuilder();
			
			code.Append($@"
	#region Property
	/// <summary>
	/// Get the underlying structural value.
	/// </summary>
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{(data.PropertyIsPublic ? "public" : "private")} {data.UnderlyingTypeName} {data.PropertyName} => this.{data.BackingFieldName}{(data is DefaultValueObject ? null : $" ?? {data.GetDefaultValue()}")};

	/// <summary>
	/// Backing field for <see cref='{data.PropertyName}'/>.{error?.Write()}
	/// </summary>
");

			if (data.GenerateDefaultConstructor)
				code.AppendLine($@"	[Obsolete(""{error}"")]");
			
			code.TrimEnd().AppendLine($@"
	[EditorBrowsable(EditorBrowsableState.Never)]
	private readonly {data.UnderlyingTypeName} {data.BackingFieldName} = {data.GetDefaultValue()};
	#endregion
");

			return code.ToString();
		}
		
		
		string? GetEqualsAndHashCode()
		{
			if (!data.GenerateComparison) return null;

			var code = new StringBuilder($@"
	#region Equals
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetHashCodeCode()} 

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetEqualsCode()}
");

			if (!data.ValueObjectType.IsRecord)
				code.TrimEnd().Append($@"
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{data.GetObjectEqualsCode()}
");

			code.AppendLine(data.ValueObjectType.IsRecord
				? null
				: $@"
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator ==({data.Name} left, {data.Name} right) => left.{data.PropertyName} == right.{data.PropertyName};
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator !=({data.Name} left, {data.Name} right) => !(left == right);
");

			code.TrimEnd().AppendLine().Append("	#endregion");
			
			return code.ToString();
		}

		
		string? GetComparison()
		{
			if (!data.GenerateComparison) 
				return null;
			
			var compareToCode = data.GetCompareToCode();
			if (compareToCode is null) 
				return null;
			
			return $@"
	#region Comparison
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{compareToCode}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <	({data.Name} left, {data.Name} right) => left.CompareTo(right) <  0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <=	({data.Name} left, {data.Name} right) => left.CompareTo(right) <= 0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >	({data.Name} left, {data.Name} right) => left.CompareTo(right) >  0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >=	({data.Name} left, {data.Name} right) => left.CompareTo(right) >= 0;
	#endregion";
		}
		
		
		string? GetStaticDefault()
		{
			if (!data.GenerateStaticDefault) 
				return null;
			
			return data.GenerateDefaultConstructor 
				? $@"
	[DebuggerHidden]
	public static {data.Name} Default {{ get; }} = new({data.GetDefaultValue()});
"
				: $@"
	[DebuggerHidden]
	public static {data.Name} Default {{ get; }} = new();
";
		}
		
		
		string GetCast()
		{
			var code = new StringBuilder();
			
			code.Append($@"
	#region Casts
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static implicit operator {data.UnderlyingTypeName}({data.Name} {data.LocalVariableName}) => {data.LocalVariableName}.{data.PropertyName};

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static explicit operator {data.Name}({data.UnderlyingTypeName} {data.LocalVariableName}) => new({data.LocalVariableName});
");
			
			var extraCastCode = data.GetExtraCastCode(); 
			
			if (extraCastCode is not null) code.AppendLine($@"
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{extraCastCode}
");

			code.AppendLine("	#endregion");
			
			return code.ToString();
		}


		string? GetEnumerator()
		{
			if (!data.GenerateEnumerable) return null;
			
			var enumeratorCode = data.GetEnumeratorCode();
			
			return enumeratorCode is null
				? null
				: @$"
	#region Enumerator
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{enumeratorCode}
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	#endregion
";
		}


		string? GetConstructors()
		{
			if (data is { GenerateDefaultConstructor: false, ForbidParameterlessConstruction: false })
				return null;

			var code = new StringBuilder().Append(@"
	#region Constructors");
			
			if (data.GenerateDefaultConstructor)
			{
				code.Append(@$"
	[DebuggerHidden] 
	public {data.ValueObjectType.Name}({data.UnderlyingTypeName} {data.LocalVariableName}, Validator? validator = null)
	{{	
");
				
				var validatorInstantiation = data.ValueObjectType.IsRefLikeType 
					? $"new Validator(objectName: typeof({data.ValueObjectType.GetTypeNameWithGenericParameters()}).Name, ValidatorMode.Default);" 
					: $"Validator.Get<{data.Name}>.Default;";
				
				code.AppendLine($"		validator ??= {validatorInstantiation}");
						
				var index = rootNamespace.IndexOf(".Domain", StringComparison.Ordinal);
				
				var boundedContextName = index > 0 
					? rootNamespace.Substring(0, index).Split('.').Last()
					: "UnknownBoundedContextName";
	
				var errorCodeStart = $"{boundedContextName}_{data.Name.Replace('<', '_').Replace(">", "")}";
				var specificValidationCode = data.GetValidationCode(errorCodeStart)?.TrimEnd();
	
				if (specificValidationCode is not null)
					code.AppendLine(specificValidationCode);
	
				code.AppendLine($@"
		this.{data.BackingFieldName} = {data.LocalVariableName}{data.GetValueTransformation()};
	}}");
			}

			if (data.ForbidParameterlessConstruction)
			{
				var error = $"Don't use this empty constructor. A {data.UnderlyingTypeName} should be provided when initializing {data.Name}.";

				code.Append($@"
	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
	[Obsolete(""{error}"", error: true)]
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public {data.ValueObjectType.Name}() => throw new InvalidOperationException($""{error}"");
	#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor
");
			}

			code.Append("	#endregion");
			
			return code.ToString();
		}
		
		
		string GetFactories()
		{
			var code = new StringBuilder(@"
	#region Factories");

			if (!data.ValueObjectType.IsRefLikeType)
			{
				code.AppendLine($@"
	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate({data.UnderlyingTypeName} {data.LocalVariableName}, {(data.NullOperator is null ? null : "[NotNullWhen(true)] ")}out {data.Name} createdObject)
		=> ICreatable<{data.Name}, {data.UnderlyingTypeName}>.TryCreate({data.LocalVariableName}, out createdObject, out _);

	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate({data.UnderlyingTypeName} {data.LocalVariableName}, {(data.NullOperator is null ? null : "[NotNullWhen(true)] ")}out {data.Name} createdObject, out Validator validator)
		=> ICreatable<{data.Name}, {data.UnderlyingTypeName}>.TryCreate({data.LocalVariableName}, out createdObject, out validator);
");
			}
			
			code.TrimEnd().AppendLine().AppendLine(@$"
	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static {data.ValueObjectType.GetTypeNameWithGenericParameters()} Create({data.UnderlyingTypeName} {data.LocalVariableName}, Validator? validator = null) 
		=> new({data.LocalVariableName}, validator);
	#endregion
");

			return code.ToString();
		}


		string? GetExtraCode()
		{
			var code = data.GetExtraCode()?.Trim();

			if (code is null)
				return null;

			return $@"
	#region TypeSpecific
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	{code}
	#endregion
";
		}
	}
}
