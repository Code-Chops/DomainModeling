using System.Collections.Immutable;
using CodeChops.SourceGeneration.Utilities;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CodeChops.DomainModeling.SourceGeneration.IdentityGenerator;

[Generator]
public class IdGenerator : IIncrementalGenerator
{
	internal const string AttributeNamespace		= "CodeChops.DomainModeling.Attributes";
	internal const string AttributeName				= "GenerateIdentity";
	internal const string EntityNamespace			= "CodeChops.DomainModeling";
	internal const string EntityName				= "Entity";
	internal const string IdNamespace				= "CodeChops.DomainModeling.Identities";
	internal const string DefaultIdTypeName			= "Identity";
	internal const string DefaultIdPropertyName		= "Id";
	internal const string DefaultIdPrimitiveType	= "global::System.UInt64";
	
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

				var fileName = model.Namespace is null ? model.OuterClassName : $"{model.Namespace}.{model.OuterClassName}";

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
		var className = $"{data.OuterClassName}{data.OuterClassGenericTypeParameters}";

		var code = new StringBuilder($@"// <auto-generated />
#nullable enable

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using CodeChops.DomainModeling.Identities;

{GetNamespaceDeclaration()}

{GetClassDeclaration()}
{{	
	{GetIdPropertyCreation()}
	{GetIdObjectCreation()}
	{GetEqualityComparison()}
}}

#nullable restore
").TrimEnd();

		return code.ToString();


		// Creates the namespace definition of the location of the enum definition (or null if the namespace is not defined).
		string? GetNamespaceDeclaration() 
			=> data.Namespace is null ? null : $@"namespace {data.Namespace};";


		string GetClassDeclaration()
		{
			var iHasIdImplementation = data.IdGenerationMethod != IdGenerationMethod.EntityBase && data.IdPropertyName == DefaultIdPropertyName 
				? " : IHasId"
				: null;

			var isClass = data.IdGenerationMethod is IdGenerationMethod.Class;
			var iIsEquatable = isClass || data.IdGenerationMethod == IdGenerationMethod.Record
				? $"{(iHasIdImplementation is null ? null : ", ")}IEquatable<{className}{(isClass ? "?" : null)}>"
				: null;
			
			var code = $"{data.OuterClassDeclaration} {className}{iHasIdImplementation}{iIsEquatable}";
			return code;
		}
		
		
		string GetIdPropertyCreation()
		{
			if (data.IdGenerationMethod == IdGenerationMethod.EntityBase)
			{
				return @$"
	[DebuggerHidden]
	public abstract IId {data.IdPropertyName} {{ get; }}
".TrimStart();
			}

			var code = $@"
	public {(data.IdGenerationMethod == IdGenerationMethod.EntityImplementation ? "override " : "")}IId {data.IdPropertyName} {{ get; }} = new {data.IdTypeName}();
".TrimStart();

			return code;
		}

		
		string? GetEqualityComparison()
		{
			if (data.IdGenerationMethod is IdGenerationMethod.EntityImplementation)
				return null;

			var code = new StringBuilder();

			var isRecord = data.IdGenerationMethod is IdGenerationMethod.Record;
			
			code.Append(@$"
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public {(isRecord ? null : "sealed")} override int GetHashCode()
	{{
		return this.Id.HasDefaultValue
			? HashCode.Combine(this)
			: this.Id.GetHashCode();
	}}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]	
	public {(isRecord ? "virtual " : null)}bool Equals({className}? other)
	{{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		if (other.GetType() != this.GetType()) return false;
		
		return !this.Id.HasDefaultValue && this.Id.Equals(other.Id);
	}}
");

			if (data.IdGenerationMethod is not IdGenerationMethod.Record)
			{
				code.Append(@$"
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public {(isRecord ? null : "sealed")} override bool Equals(object? obj)
	{{
		return obj is {className} other 
		       && obj.GetType() == this.GetType() 
		       && this.Equals(other);
	}}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator ==({className}? left, {className}? right)
	{{
		if (left is null && right is null) return true;
		if (left is null || right is null) return false;
		return left.Equals(right);
	}}
	
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator !=({className}? left, {className}? right) 
		=> !(left == right);
");
			}
			
			return code.ToString().Trim();
		}
		
		
		string? GetIdObjectCreation()
		{
			if (data.IdGenerationMethod == IdGenerationMethod.EntityBase)
				return null;
			
			var code = $@"
	public readonly partial record struct {data.IdTypeName} : IId<{data.IdTypeName}, {data.PrimitiveTypeFullName}>
	{{ 
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => this.ToDisplayString(new {{ this.Value, PrimitiveType = nameof({data.PrimitiveTypeFullName}) }});

		[EditorBrowsable(EditorBrowsableState.Never)]
		public {data.PrimitiveTypeFullName} Value {{ get; private init; }}
	
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static explicit operator {data.IdTypeName}({data.PrimitiveTypeFullName} value) => new() {{ Value = value }};
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static implicit operator {data.PrimitiveTypeFullName}({data.IdTypeName}{data.NullOperator} id) => id.Value;
	
		#region Comparison
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int CompareTo({data.IdTypeName} other) 
			=> {(data.NullOperator is null ? null : "other is null ? 1 : ")}this.Value.CompareTo(({data.IdTypeName})other.GetValue());
		
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <	({data.IdTypeName} left, {data.IdTypeName}{data.NullOperator} right)	=> left.CompareTo(right) <	0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=	({data.IdTypeName} left, {data.IdTypeName}{data.NullOperator} right)	=> left.CompareTo(right) <= 0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >	({data.IdTypeName} left, {data.IdTypeName}{data.NullOperator} right)	=> left.CompareTo(right) >	0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=	({data.IdTypeName} left, {data.IdTypeName}{data.NullOperator} right)	=> left.CompareTo(right) >= 0;
		#endregion
	
		/// <summary>
		/// Warning. Probably performs boxing!
		/// </summary>
		[DebuggerHidden]
		public object GetValue() => this.Value;
	
		[DebuggerHidden]
		public bool HasDefaultValue => this.Value.Equals(IId<{data.PrimitiveTypeFullName}>.DefaultValue);
	
		[DebuggerHidden]
		public {data.IdTypeName}({data.PrimitiveTypeFullName} value)
		{{
			this.Value = value;
		}}
		
		[DebuggerHidden]
		public {data.IdTypeName}()
		{{
			this.Value = default({data.PrimitiveTypeFullName});
		}}
	}}
";

			return code.TrimStart();
		}
	}
}
