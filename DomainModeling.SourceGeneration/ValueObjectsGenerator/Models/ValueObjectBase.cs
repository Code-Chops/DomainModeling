namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

/// <param name="ValueObjectType">The type of the partial class being generated.</param>
/// <param name="TypeDeclarationSyntax">The declaration of the class being generated </param>
/// <param name="UnderlyingTypeName"></param>
/// <param name="GenerateToString"></param>
/// <param name="GenerateComparison"></param>
/// <param name="AddCustomValidation"></param>
/// <param name="GenerateDefaultConstructor"></param>
/// <param name="AddParameterlessConstructor"></param>
/// <param name="GenerateStaticDefault"></param>
/// <param name="PropertyName"></param>
/// <param name="AddIComparable"></param>
public abstract record ValueObjectBase(
	// ReSharper disable once NotAccessedPositionalProperty.Global
	INamedTypeSymbol ValueObjectType,
	TypeDeclarationSyntax TypeDeclarationSyntax,
	string UnderlyingTypeName,
	string? UnderlyingTypeNameBase,
	bool GenerateToString, 
	bool GenerateComparison,
	bool AddCustomValidation,
	bool GenerateDefaultConstructor,
	bool AddParameterlessConstructor,
	bool GenerateStaticDefault,
	bool GenerateEnumerable,
	string PropertyName,
	bool PropertyIsPublic,
	bool AddIComparable)
{
	public bool IsUnsealedRecordClass { get; } = ValueObjectType.IsRecord && ValueObjectType.TypeKind is not TypeKind.Struct && !ValueObjectType.IsSealed;

	/// <summary>
	/// Null conditional operator.
	/// </summary>
	public string? NullOperator { get; } = ValueObjectType.TypeKind is not TypeKind.Struct ? "?" : null;
	
	/// <summary>
	/// The name of the partial class being generated.
	/// </summary>
	public string Name { get; } = ValueObjectType.GetTypeNameWithGenericParameters();
	public string? Namespace { get; } = ValueObjectType.ContainingNamespace!.IsGlobalNamespace ? null : ValueObjectType.ContainingNamespace.ToDisplayString();

	public string UnderlyingTypeNameBase { get; } = UnderlyingTypeNameBase ?? UnderlyingTypeName;
	
	/// <summary>
	/// Has a different name each time it's generated. In order to prohibit direct usage of the backing field.
	/// </summary>
	public string BackingFieldName { get; } = $"_{PropertyName.Substring(0, 1).ToLowerInvariant()}{PropertyName.Substring(1)}{new Random().Next(0, 9999)}";
	public string LocalVariableName { get; } = PropertyName.Substring(0, 1).ToLowerInvariant() + PropertyName.Substring(1);

	public abstract string[] GetNamespaces();
	public abstract string GetCommentsCode();
	public abstract string GetToStringCode();
	public abstract string? GetInterfacesCode();
	public abstract string GetHashCodeCode();
	public abstract string GetEqualsCode();
	public abstract string GetObjectEqualsCode();
	public abstract string? GetCompareToCode();
	public abstract string GetDefaultValue();
	public abstract string? GetLengthOrCountCode();
	public abstract string? GetExtraCastCode();
	public abstract string GetValidationCode();
	public abstract string? GetEnumeratorCode();
	/// <summary>
	/// Don't forget to place [EditorBrowsable(EditorBrowsableState.Never)] and/or [DebuggerHidden] at the <b>extra</b> lines.
	/// </summary>
	public abstract string? GetExtraCode();
}
