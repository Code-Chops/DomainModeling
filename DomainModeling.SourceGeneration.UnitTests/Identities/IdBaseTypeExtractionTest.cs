using CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.IdentityGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.Identities;

public class IdBaseTypeExtractionTest
{
	private static PortableExecutableReference MsCoreLib { get; }
	
	static IdBaseTypeExtractionTest()
	{
		MsCoreLib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
	}
	
	[Theory]
	[InlineData("[GenerateStronglyTypedId]", 						"IId<UInt64>",	"UInt64")]
	[InlineData("[GenerateStronglyTypedId<ulong>]",					"IId<UInt64>",	"UInt64")]				
	[InlineData("[GenerateStronglyTypedId<string>]",				"IId<string>",	"string")] 	
	[InlineData("[GenerateStronglyTypedId<string>(typeof(Guid))]",	"IId<string>",	"string")]
	[InlineData("[GenerateStronglyTypedId<Tuple>(typeof(Tuple))]",	"IId<Tuple>",	"Tuple")]
	public void IdType_Extraction_IsCorrect(string attribute, string expectedBaseType, string expectedPrimitiveType)
	{
		var syntaxTree = GetSyntaxTree(attribute);
		
		var compilation = CSharpCompilation.Create(
			assemblyName: nameof(IdBaseTypeExtractionTest),
			syntaxTrees: new[] { syntaxTree }, 
			references: new[] { MsCoreLib });

		var classDeclarationSyntax = syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();
		var semanticModel = compilation.GetSemanticModel(syntaxTree);

		var model = IdSyntaxReceiver.GetModel(classDeclarationSyntax, semanticModel);
		
		Assert.NotNull(model);
		
		Assert.Equal(expectedBaseType, model.IdBaseType, StringComparer.OrdinalIgnoreCase);
		Assert.Equal(expectedPrimitiveType, model.IdPrimitiveType, StringComparer.OrdinalIgnoreCase);
	}

	private static SyntaxTree GetSyntaxTree(string attribute)
	{
		var code = $@"
using System;
using {IdGenerator.AttributeNamespace};
using {IdGenerator.IdNamespace};

namespace CodeChops.Test
{{

	{attribute}
	public partial class TestClass
	{{
	}}
}}

namespace {IdGenerator.AttributeNamespace}
{{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class GenerateStronglyTypedId<TId> : Attribute
		where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
	{{
		public GenerateStronglyTypedId(StringFormat? baseType = null, string? name = null)
		{{
		}}
	}}
	
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class GenerateStronglyTypedId : Attribute
	{{
		public GenerateStronglyTypedId(string? name = null)
		{{
		}}
	}}
}}

namespace {IdGenerator.IdNamespace}
{{
	public abstract record Guid<TSelf> : Id<TSelf, string>
		where TSelf : Guid<TSelf>
	{{
	    private static readonly Regex ValidationRegex = new(""^[0-9A-F]{{32}}$"", RegexOptions.Compiled);
	
	    protected Guid(string guid)
	        : base(guid)
	    {{
	    }}
	
	    protected Guid()
	        : base(Guid.NewGuid().ConvertToString())
	    {{
	    }}
	}}
}}
";

		return CSharpSyntaxTree.ParseText(code);
	}
}