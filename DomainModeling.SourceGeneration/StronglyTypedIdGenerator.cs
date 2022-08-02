﻿using System.Collections.Immutable;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration;

[Generator]
public class StronglyTypedIdGenerator : IIncrementalGenerator
{
	internal const string AttributeNamespace	= "CodeChops.DomainDrivenDesign.DomainModeling.Attributes";
	internal const string AttributeName			= "GenerateStronglyTypedId";
	internal const string EntityNamespace		= "CodeChops.DomainDrivenDesign.DomainModeling";
	internal const string EntityName			= "Entity";
	
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{		
		var valueProvider = context.SyntaxProvider
			.CreateSyntaxProvider(
				predicate: SyntaxReceiver.CheckIfProbablyNeedsStronglyTypedId,
				transform: SyntaxReceiver.GetModel)
			.Where(static model => model is not null)
			.Collect();
		
		context.RegisterSourceOutput(source: valueProvider, action: CreateSource!);
	}
	
	private static void CreateSource(SourceProductionContext context, ImmutableArray<DataModel> models)
	{
		foreach (var model in models)
		{
			var code = CreateSource(model);

			var fileName = model.Namespace is null 
				? model.Name 
				: $"{model.Namespace}.{model.Name}";
			
			context.AddSource($"{fileName}.g.cs", SourceText.From(code, Encoding.UTF8));
		}
	}

	private static string CreateSource(DataModel data)
	{
		var className = data.Name;

		var idName = $"{className}Id";

		var code = $@"// <auto-generated />
#nullable enable
using System;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;
{GetNamespaceDeclaration()}

{data.Declaration} {className}
{{	{GetIdCreation()}
	{GetEqualityComparison()}
}}

#nullable restore
";

		return code;
		
		
		// Creates the namespace definition of the location of the enum definition (or null if the namespace is not defined).
		string? GetNamespaceDeclaration()
		{
			if (data.Namespace is null) return null;

			var code = $@"namespace {data.Namespace};";
			return code;
		}

		string GetIdCreation()
		{
			if (data.GenerationMethod == GenerationMethod.EntityBase)
			{
				return $@"
	public abstract Id Id {{ get; }}
";
			}

			var code = $@"
	public {(data.GenerationMethod == GenerationMethod.EntityImplementation ? "override " : "")}{idName} Id {{ get; }} = new();

	public partial record {idName} : global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Id<{idName}, {data.IdIntegralType}> 
	{{ 
		public {idName}({data.IdIntegralType} value) : base(value) {{ }}
		public {idName}() : base() {{ }}
	}}";

			return code;
		}

		string GetEqualityComparison()
		{
			if (data.GenerationMethod is GenerationMethod.EntityImplementation or GenerationMethod.Record)
				return "";
					
			var code = @$"
	public sealed override int GetHashCode()
	{{
		return this.Id.HasDefaultValue
			? HashCode.Combine(this)
			: this.Id.GetHashCode();
	}}
	
	public sealed override bool Equals(object? obj)
	{{
		return obj is {className} other 
		       && obj.GetType() == this.GetType() 
		       && this.Equals(other);
	}}

	public bool Equals({className}? other)
	{{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		if (other.GetType() != this.GetType()) return false;
		
		return !this.Id.HasDefaultValue && this.Id.Equals(other.Id);
	}}";

			return code;
		}
		
	}
}
