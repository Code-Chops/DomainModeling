namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

/// <param name="ValueObjectType">The type of the partial class being generated.</param>
public abstract record ValueObjectBase(
// ReSharper disable once NotAccessedPositionalProperty.Global
	bool UseValidationExceptions,
	INamedTypeSymbol ValueObjectType,
	string UnderlyingTypeName,
	string? UnderlyingTypeNameBase,
	bool GenerateToString, 
	bool GenerateComparison,
	bool AddCustomValidation,
	bool ConstructorIsPublic,
	bool ForbidParameterlessConstruction,
	bool GenerateStaticDefault,
	bool GenerateEnumerable,
	string PropertyName,
	bool PropertyIsPublic,
	bool AddIComparable,
	bool AllowNull)
{
	public bool IsUnsealedRecordClass { get; } = ValueObjectType.IsRecord && ValueObjectType.TypeKind is not TypeKind.Struct && !ValueObjectType.IsSealed;

	/// <summary>
	/// Null conditional operator for the value object.
	/// </summary>
	public string? NullOperator { get; } = ValueObjectType.TypeKind is not TypeKind.Struct ? "?" : null;
	
	/// <summary>
	/// The name of the value object being generated.
	/// </summary>
	public string Name { get; } = ValueObjectType.GetTypeNameWithGenericParameters();
	public string? Namespace { get; } = ValueObjectType.ContainingNamespace!.IsGlobalNamespace ? null : ValueObjectType.ContainingNamespace.ToDisplayString();

	public string UnderlyingTypeNameBase { get; } = UnderlyingTypeNameBase ?? UnderlyingTypeName;
	
	/// <summary>
	/// Has a different name each time it's generated. In order to prohibit direct usage of the backing field.
	/// </summary>
	public string BackingFieldName { get; } = $"_{PropertyName.Substring(0, 1).ToLowerInvariant()}{PropertyName.Substring(1)}{new Random().Next(0, 9999)}";
	public string LocalVariableName { get; } = PropertyName.Substring(0, 1).ToLowerInvariant() + PropertyName.Substring(1);

	public List<string> ErrorCodes { get; } = new();

	public enum Guard
	{
		NotNull,
		InRange,
		Regex,
	}
	
	public string GetGuardLine<T>(Guard guard, string? valueName, string errorCodeStart, params object?[] parameters)
		=> this.GetGuardLine(guard, valueName: valueName, errorCodeStart, genericParameterName: typeof(T).Name, parameters);
	
	public string GetGuardLine(Guard guard, string? valueName, string errorCodeStart, string? genericParameterName = null, params object?[] parameters)
	{
		var errorCode = "null";
		if (this.UseValidationExceptions)
		{
			errorCode = $@"errorCode: ErrorCode_{errorCodeStart}_{guard switch
			{
				Guard.NotNull	=> "Null",
				Guard.InRange	=> "OutOfRange",
				Guard.Regex		=> "Regex",
				_				=> "_Unknown_",
			}}";
		
			this.ErrorCodes.Add(errorCode);
		}

		var stringParameters = new[] { valueName ?? this.LocalVariableName }.Concat(parameters.Select(p => p?.ToString() ?? "null")).Append(errorCode);
		var parametersString = String.Join(", ", stringParameters);
		
		return $@"			.Guard{guard}{genericParameterName?.Write($"<{genericParameterName}>")}({parametersString})";
	}
	
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
	public abstract string? GetValidationCode(string errorCodeStart);
	public abstract string? GetValueTransformation();
	public abstract string? GetEnumeratorCode();
	/// <summary>
	/// Don't forget to place [EditorBrowsable(EditorBrowsableState.Never)] and/or [DebuggerHidden] at the each extra line.
	/// </summary>
	public abstract string? GetExtraCode();
}
