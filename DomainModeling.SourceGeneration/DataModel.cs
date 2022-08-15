namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration;

public record DataModel(string OuterClassName, string? OuterClassGenericTypeParameters, string? Namespace, string OuterClassDeclaration, string IdName, string IdPrimitiveType, string IdBaseType, GenerationMethod GenerationMethod);