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
	[InlineData("[GenerateIdentity]", 						"global::System.UInt64")]
	[InlineData("[GenerateIdentity<ulong>]",					"global::System.UInt64")]				
	[InlineData("[GenerateIdentity<string>]",				"global::System.String")] 	
	[InlineData("[GenerateIdentity<string>(typeof(Guid))]",	"global::System.String")]
	[InlineData("[GenerateIdentity<Tuple>(typeof(Tuple))]",	"global::System.Tuple")]
	public void IdType_Extraction_IsCorrect(string attribute, string expectedPrimitiveType)
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
		
		Assert.Equal(expectedPrimitiveType, model.PrimitiveTypeFullName, StringComparer.OrdinalIgnoreCase);
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
	public sealed class GenerateIdentity<TId> : Attribute
		where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
	{{
		public GenerateIdentity(StringFormat? baseType = null, string? name = null)
		{{
		}}
	}}
	
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class GenerateIdentity : Attribute
	{{
		public GenerateIdentity(string? name = null)
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
