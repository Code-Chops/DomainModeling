namespace CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public abstract record ValueObjectBase(
	INamedTypeSymbol ValueObjectType,
	bool GenerateToString,
	bool GenerateComparison,
	bool GenerateDefaultConstructor,
	bool ForbidParameterlessConstruction,
	bool GenerateStaticDefault,
	bool GenerateEnumerable,
	string PropertyName,
	bool PropertyIsPublic,
	bool AllowNull,
	bool UseValidationExceptions,
	bool AddIComparable,
	bool UseCustomProperty)
{
	public string? ErrorMessage { get; protected set; }

	public abstract string UnderlyingTypeName { get; }
	public abstract string? UnderlyingTypeNameBase { get; }
	public char? UnderlyingTypeNullOperator { get; } = AllowNull ? '?' : null;

	public bool IsUnsealedRecordClass { get; } = ValueObjectType is { IsRecord: true, TypeKind: not TypeKind.Struct, IsSealed: false };

	/// <summary>
	/// Null conditional operator for the value object.
	/// </summary>
	public char? NullOperator { get; } = ValueObjectType.TypeKind is TypeKind.Class || AllowNull ? '?' : null;

	/// <summary>
	/// The name of the value object being generated.
	/// </summary>
	public string Name { get; } = ValueObjectType.GetTypeNameWithGenericParameters();

	public string? Namespace { get; } = ValueObjectType.ContainingNamespace!.IsGlobalNamespace ? null : ValueObjectType.ContainingNamespace.ToDisplayString();

	/// <summary>
	/// Has a different name each time it's generated. In order to prohibit direct usage of the backing field.
	/// </summary>
	public string BackingFieldName { get; } = "_value";

	public string LocalVariableName { get; } = PropertyName.Substring(0, 1).ToLowerInvariant() + PropertyName.Substring(1);

	public List<string> ErrorCodes { get; } = new();

	public bool HasEmptyConstructor => !this.ForbidParameterlessConstruction && this.ValueObjectType.TypeKind == TypeKind.Structure;

	public enum Guard
	{
		NotNull,
		InRange,
		LengthInRange,
		Regex,
	}

	protected static IEnumerable<string> GetAllUsingNamespacesOfType(INamedTypeSymbol type)
	{
		if (!type.TypeArguments.IsDefaultOrEmpty)
			foreach (var s in type.TypeArguments.OfType<INamedTypeSymbol>().SelectMany(GetAllUsingNamespacesOfType))
				yield return s;

		yield return type.ContainingNamespace?.IsGlobalNamespace ?? true
			? "System"
			: type.ContainingNamespace.ToDisplayString();
	}

	public string GetGuardLine<T>(Guard guard, string? variableName, string errorCodeStart, params object?[] parameters)
		=> this.GetGuardLine(guard, variableName: variableName, errorCodeStart, genericParameterName: typeof(T).Name, parameters);

	public string GetGuardLine(Guard guard, string? variableName, string errorCodeStart, string? genericParameterName = null, params object?[] parameters)
	{
		var errorCode = "errorCode: null";
		if (this.UseValidationExceptions)
		{
			errorCode = $"errorCode: ErrorCode_{errorCodeStart}_{guard switch
			{
				Guard.NotNull		=> "Null",
				Guard.InRange		=> "OutOfRange",
				Guard.LengthInRange	=> "LengthOutOfRange",
				Guard.Regex			=> "Regex",
				_					=> "_Unknown_",
			}}";

			this.ErrorCodes.Add(errorCode);
		}

		var stringParameters = new[] { variableName ?? this.LocalVariableName }.Concat(parameters.Select(p => p?.ToString() ?? "null")).Append(errorCode);
		var parametersString = String.Join(", ", stringParameters);

		return $@"		validator.Guard{guard}{genericParameterName?.Write($"<{genericParameterName}>")}({parametersString});";
	}

	public abstract IEnumerable<string> GetUsingNamespaces();
	public abstract string GetComments();
	public abstract string GetToStringCode();
	public abstract string? GetInterfacesCode();
	public abstract string GetHashCodeCode();
	public abstract string GetEqualsCode();
	public abstract string GetObjectEqualsCode();
	public abstract string? GetCompareToCode();
	public abstract string GetDefaultValue();
	public abstract string? GetLengthOrCountCode();
	public abstract string? GetExtraCastCode();
	public abstract string? GetExtraConstructorCode();
	public abstract string? GetValidationCode(string errorCodeStart);
	public abstract string? GetValueTransformation();
	public abstract string? GetEnumeratorCode();
	/// <summary>
	/// Don't forget to place [EditorBrowsable(EditorBrowsableState.Never)] and/or [DebuggerHidden] at the each extra line, if needed.
	/// </summary>
	public abstract string? GetExtraCode();
}
