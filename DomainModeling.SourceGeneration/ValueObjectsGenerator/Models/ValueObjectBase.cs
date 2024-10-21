namespace CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public abstract record ValueObjectBase
{
	protected ValueObjectBase(
		INamedTypeSymbol valueObjectType,
		bool generateToString,
		bool generateComparison,
		bool generateDefaultConstructor,
		bool forbidParameterlessConstruction,
		bool generateStaticDefault,
		bool generateEnumerable,
		string propertyName,
		bool propertyIsPublic,
		bool allowNull,
		bool useValidationExceptions,
		bool addIComparable,
		bool useCustomProperty)
	{
		this.ValueObjectType = valueObjectType;
		this.GenerateToString = generateToString;
		this.GenerateComparison = generateComparison;
		this.GenerateDefaultConstructor = generateDefaultConstructor;
		this.ForbidParameterlessConstruction = forbidParameterlessConstruction;
		this.GenerateStaticDefault = generateStaticDefault;
		this.GenerateEnumerable = generateEnumerable;
		this.PropertyName = propertyName;
		this.PropertyIsPublic = propertyIsPublic;
		this.AllowNull = allowNull;
		this.UseValidationExceptions = useValidationExceptions;
		this.AddIComparable = addIComparable;
		this.IsUnsealedRecordClass = valueObjectType is { IsRecord: true, TypeKind: not TypeKind.Struct, IsSealed: false };
		this.ValueObjectNullOperator = valueObjectType.TypeKind is TypeKind.Class || allowNull ? '?' : null;
		this.UnderlyingTypeNullOperator = allowNull ? '?' : null;

		this.Name = valueObjectType.GetTypeNameWithGenericParameters();
		this.Namespace = valueObjectType.ContainingNamespace!.IsGlobalNamespace ? null : valueObjectType.ContainingNamespace.ToDisplayString();
		this.BackingFieldName = "_value";
		this.LocalVariableName = propertyName.Substring(0, 1).ToLowerInvariant() + propertyName.Substring(1);
		this.UseCustomProperty = useCustomProperty;
	}

	public string? ErrorMessage { get; protected set; }

	public abstract string UnderlyingTypeName { get; }
	public abstract string? UnderlyingTypeNameBase { get; }
	public char? UnderlyingTypeNullOperator { get; }

	public bool IsUnsealedRecordClass { get; }

	/// <summary>
	/// Null conditional operator for the value object.
	/// </summary>
	public char? ValueObjectNullOperator { get; }

	/// <summary>
	/// The name of the value object being generated.
	/// </summary>
	public string Name { get; }
	public string? Namespace { get; }

	/// <summary>
	/// Has a different name each time it's generated. In order to prohibit direct usage of the backing field.
	/// </summary>
	public string BackingFieldName { get; }
	public string LocalVariableName { get; }

	public List<string> ErrorCodes { get; } = new();

	public INamedTypeSymbol ValueObjectType { get; }

	public bool GenerateToString { get; }
	public bool GenerateComparison { get; }
	public bool GenerateDefaultConstructor { get; }
	public bool ForbidParameterlessConstruction { get; }
	public bool GenerateStaticDefault { get; }
	public bool GenerateEnumerable { get; }
	public string PropertyName { get; }
	public bool PropertyIsPublic { get; }
	public bool AllowNull { get; }
	public bool UseValidationExceptions { get; }
	public bool AddIComparable { get; }
	public bool UseCustomProperty { get; }

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
