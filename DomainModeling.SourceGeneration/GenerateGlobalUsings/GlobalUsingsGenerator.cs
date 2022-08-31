﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.GenerateGlobalUsings;

[Generator]
public class GlobalUsingsGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(i =>
		{
			const string globalUsingsCode = @"// <auto-generated />
global using CodeChops.DomainDrivenDesign.DomainModeling.Attributes;
global using CodeChops.DomainDrivenDesign.DomainModeling.Attributes.ValueObjects;
global using CodeChops.DomainDrivenDesign.DomainModeling.Collections;
global using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;
global using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;
global using CodeChops.DomainDrivenDesign.DomainModeling.Extensions;
global using CodeChops.DomainDrivenDesign.DomainModeling.Factories;
global using CodeChops.DomainDrivenDesign.DomainModeling.Helpers;
global using CodeChops.DomainDrivenDesign.DomainModeling.Identities;
global using CodeChops.DomainDrivenDesign.DomainModeling;
";

			i.AddSource("GlobalUsings.g.cs", globalUsingsCode);
		});
	}
}