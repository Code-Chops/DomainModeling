﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.GlobalUsingsGenerator;

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
global using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;
global using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System;
global using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.System.Core;
global using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User;
global using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User.Core;
global using CodeChops.DomainDrivenDesign.DomainModeling.Factories;
global using CodeChops.DomainDrivenDesign.DomainModeling.Identities;
global using CodeChops.DomainDrivenDesign.DomainModeling.TypeExtensions;
global using CodeChops.DomainDrivenDesign.DomainModeling.Validation;
global using CodeChops.DomainDrivenDesign.DomainModeling;
";

			i.AddSource("GlobalUsings.g.cs", globalUsingsCode);
		});
	}
}