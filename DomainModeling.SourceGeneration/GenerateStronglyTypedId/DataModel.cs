namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration;

public record DataModel(string OuterClassName, string? OuterClassGenericTypeParameters, string? Namespace, string OuterClassDeclaration, 
	string IdTypeName, string IdPropertyName, string IdPrimitiveType, string IdBaseType, GenerationMethod GenerationMethod);