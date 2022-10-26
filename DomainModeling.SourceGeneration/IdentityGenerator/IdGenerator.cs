﻿using System.Collections.Immutable;
using CodeChops.SourceGeneration.Utilities;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.IdentityGenerator;

[Generator]
public class IdGenerator : IIncrementalGenerator
{
	internal const string AttributeNamespace		= "CodeChops.DomainDrivenDesign.DomainModeling.Attributes";
	internal const string AttributeName		= "GenerateStronglyTypedId";
	internal const string EntityNamespace			= "CodeChops.DomainDrivenDesign.DomainModeling";
	internal const string EntityName				= "Entity";
	internal const string IdNamespace				= "CodeChops.DomainDrivenDesign.DomainModeling.Identities";
	internal const string DefaultIdTypeName			= "Identity";
	internal const string DefaultIdPropertyName		= "Id";
	internal const string DefaultIdPrimitiveType	= "UInt64";
	
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
		
#pragma warning disable CS0168
		catch (Exception e)
#pragma warning restore CS0168
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
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;
{GetPrimitiveTypeUsing()}
{GetNamespaceDeclaration()}

{GetClassDeclaration()}
{{	
	{GetIdPropertyCreation()}
	{GetIdObjectCreation(data.IdGenerationMethod, data.IdTypeName, data.IdBaseType, data.IdPrimitiveType)}
	{GetEqualityComparison()}
}}

#nullable restore
").TrimEnd();

		return code.ToString();


		string? GetPrimitiveTypeUsing()
		{
			return data.PrimitiveTypeNamespace is null 
				? null
				: $"using {data.PrimitiveTypeNamespace};";
		}
		
		
		// Creates the namespace definition of the location of the enum definition (or null if the namespace is not defined).
		string? GetNamespaceDeclaration()
		{
			if (data.Namespace is null) return null;

			var code = $@"namespace {data.Namespace};";
			return code;
		}

		
		string GetClassDeclaration()
		{
			var iHasIdImplementation = data.IdGenerationMethod == IdGenerationMethod.EntityBase || data.IdPropertyName != DefaultIdPropertyName 
				? null
				: " : IHasId";
			
			var code = $"{data.OuterClassDeclaration} {className}{iHasIdImplementation}";
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
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
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
		
		
		static string? GetIdObjectCreation(IdGenerationMethod generationMethod, string idName, string idBaseType, string? primitiveType)
		{
			if (generationMethod == IdGenerationMethod.EntityBase)
				return null;
			
			var code = $@"
	public readonly partial record struct {idName} : {idBaseType}
	{{ 
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => this.ToDisplayString(new {{ this.Value, PrimitiveType = nameof({primitiveType}) }});

		[EditorBrowsable(EditorBrowsableState.Never)]
		public {primitiveType} Value {{ get; private init; }}
	
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static explicit operator {idName}({primitiveType} value) => new() {{ Value = value }};
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static implicit operator {primitiveType}({idName} id) => id.Value;
	
		#region Comparison
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int CompareTo(IId? other) 
			=> other is null ? 1 : this.Value.CompareTo(({primitiveType})other.GetValue());
		
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <	({idName} left, IId right)	=> left.CompareTo(right) <	0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=	({idName} left, IId right)	=> left.CompareTo(right) <= 0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >	({idName} left, IId right)	=> left.CompareTo(right) >	0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=	({idName} left, IId right)	=> left.CompareTo(right) >= 0;
		#endregion
	
		/// <summary>
		/// Warning. Probably performs boxing!
		/// </summary>
		[DebuggerHidden]
		public object GetValue() => this.Value;
	
		[DebuggerHidden]
		public bool HasDefaultValue => this.Value.Equals(IId<{primitiveType}>.DefaultValue);
	
		[DebuggerHidden]
		public {idName}({primitiveType} value)
		{{
			this.Value = value;
		}}
		
		[DebuggerHidden]
		public {idName}()
		{{
			this.Value = default({primitiveType});
		}}
	}}
";

			return code.TrimStart();
		}
	}
}
