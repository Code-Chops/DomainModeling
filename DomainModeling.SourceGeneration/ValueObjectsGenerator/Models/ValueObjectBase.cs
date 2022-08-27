namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public abstract record ValueObjectBase(
	// ReSharper disable once NotAccessedPositionalProperty.Global
	INamedTypeSymbol Type,
	string Declaration,
	string TypeName,
	string ElementTypeName,
	bool GenerateToString, 
	bool AddCustomValidation,
	bool ProhibitParameterlessConstruction,
	bool GenerateEmptyStatic,
	bool GenerateComparable)
{
	public bool IsUnsealedRecordClass { get; } = Type.IsRecord && Type.TypeKind is not TypeKind.Struct && !Type.IsSealed;
	public string? Nullable { get; } = Type.TypeKind is TypeKind.Struct ? null : "?";
	public string Name { get; } = Type.Name;
	public string? Namespace { get; } = Type.ContainingNamespace!.IsGlobalNamespace ? null : Type.ContainingNamespace.ToDisplayString();

	public abstract string? GetNamespaces();
	public abstract string GetCommentsCode();
	public abstract string GetToStringCode();
	public abstract string? GetInterfacesCode();
	public abstract string GetHashCodeCode();
	public abstract string GetEqualsCode();
	public abstract string? GetCompareToCode();
	public abstract string GetDefaultValue();
	public abstract string? GetLengthOrCountCode();
	public abstract string GetValidationCode();
	public abstract string? GetEnumeratorCode();
	public abstract string? GetExtraCode();
}