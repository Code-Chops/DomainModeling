using System.Collections.Immutable;
using CodeChops.SourceGeneration.Utilities;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CodeChops.DomainModeling.SourceGeneration.IdentityGenerator;

[Generator]
public class IdGenerator : IIncrementalGenerator
{
	internal const string AttributeNamespace		= "CodeChops.DomainModeling.Attributes";
	internal const string AttributeName				= "GenerateIdentity";
	internal const string IdNamespace				= "CodeChops.DomainModeling.Identities";
	internal const string DefaultIdPropertyName		= "Id";
	internal const string DefaultIdUnderlyingType	= "global::System.UInt64";
	
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{		
		var valueProvider = context.SyntaxProvider
			.CreateSyntaxProvider(
				predicate: IdSyntaxReceiver.CheckIfProbablyNeedsStronglyTypedId,
				transform: static (context, _) => IdSyntaxReceiver.GetModel((TypeDeclarationSyntax)context.Node, context.SemanticModel))
			.Where(static model => model is not null)
			.Collect()
			.Combine(context.AnalyzerConfigOptionsProvider);
		
		context.RegisterSourceOutput(source: valueProvider, action: static (context, valueProvider) => CreateSource(context, valueProvider.Left!, valueProvider.Right));
	}
	
	private static void CreateSource(SourceProductionContext context, ImmutableArray<IdDataModel> models, AnalyzerConfigOptionsProvider configOptionsProvider)
	{
		try
		{
			foreach (var model in models)
			{
				var code = CreateSource(model);

				var fileName = model.Namespace is null ? model.IdTypeName : $"{model.Namespace}.{model.IdTypeName}";

				fileName = $"{fileName}.{model.IdTypeName}";
				fileName = FileNameHelpers.GetFileName(fileName, configOptionsProvider);

				context.AddSource(fileName, SourceText.From(code, Encoding.UTF8));
			}
		}
		
		catch (Exception e)
		{
			var descriptor = new DiagnosticDescriptor(nameof(IdGenerator), "Error", $"{nameof(IdGenerator)} failed to generate due to an error. Please inform CodeChops (www.CodeChops.nl). Error: {e}", "Compilation", DiagnosticSeverity.Error, isEnabledByDefault: true);
			context.ReportDiagnostic(Diagnostic.Create(descriptor, null));

			context.AddSource($"{nameof(IdGenerator)}_Exception_{Guid.NewGuid()}", SourceText.From($"/*{e}*/", Encoding.UTF8));
		}
	}

	private static string CreateSource(IdDataModel data)
	{
		var code = new StringBuilder($@"// <auto-generated />
#nullable enable

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CodeChops.DomainModeling.Identities;
");

		code.AppendLine(GetNamespaceDeclaration);

		code.AppendLine(GetIdObjectCreation, trimEnd: true);
		code.AppendLine(@"
#nullable restore
".Trim());

		return code.ToString();


		// Creates the namespace definition of the location of the enum definition (or null if the namespace is not defined).
		string? GetNamespaceDeclaration() 
			=> data.Namespace is null ? null : $@"
namespace {data.Namespace};
";
		

		string? GetIdObjectCreation()
		{
			var code = $@"
[StructLayout(LayoutKind.Auto)]
public readonly partial record struct {data.IdTypeName} : IId<{data.IdTypeName}, {data.UnderlyingTypeFullName}>
{{ 
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => this.Value.ToString()!;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public {data.UnderlyingTypeFullName} Value {{ get; private init; }}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static explicit operator {data.IdTypeName}({data.UnderlyingTypeFullName} value) => new() {{ Value = value }};
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static implicit operator {data.UnderlyingTypeFullName}({data.IdTypeName} id) => id.Value;

	#region Comparison
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int CompareTo({data.IdTypeName} other) => Comparer<{data.UnderlyingTypeFullName}>.Default.Compare(this.Value, other.Value);
	
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <	({data.IdTypeName} left, {data.IdTypeName} right)	=> left.CompareTo(right) <	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=	({data.IdTypeName} left, {data.IdTypeName} right)	=> left.CompareTo(right) <= 0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >	({data.IdTypeName} left, {data.IdTypeName} right)	=> left.CompareTo(right) >	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=	({data.IdTypeName} left, {data.IdTypeName} right)	=> left.CompareTo(right) >= 0;
	#endregion

	[DebuggerHidden]
	bool IId.HasDefaultValue => this.Value == default;

	[DebuggerHidden]
	public {data.IdTypeName}({data.UnderlyingTypeFullName} value)
	{{
		this.Value = value;
	}}

	/// <summary>
	/// Initializes the ID with a default value.
	/// </summary>
	[DebuggerHidden]
	public static {data.IdTypeName} Create(Validator? validator = null)
	{{
		return new(default);
	}}
}}
";

			return code;
		}
	}
}
