namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.Models;

public record EntityModel
{
	public string Name { get; }
	public string? Namespace { get; }
	public string Declaration { get; }
	public string IdIntegralType { get; }

	public EntityModel(ITypeSymbol type, string idIntegralType)
	{
		this.Name = type.Name;
		this.Namespace = type.ContainingNamespace!.IsGlobalNamespace 
			? null 
			: type.ContainingNamespace.ToDisplayString();
		this.Declaration = type.GetObjectDeclaration();
		this.IdIntegralType = idIntegralType;
	}
}