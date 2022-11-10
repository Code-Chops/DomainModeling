namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record DictionaryValueObject(
		bool UseValidationExceptions,
		int? MinimumCount,
		int? MaximumCount,
		INamedTypeSymbol ValueObjectType,
		ITypeSymbol KeyType,
		ITypeSymbol ValueType,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool ConstructorIsPublic,
		bool ForbidParameterlessConstruction,
		bool GenerateStaticDefault,
		bool GenerateEnumerable,
		string? PropertyName,
		bool PropertyIsPublic,
		bool AllowNull) 
	: ValueObjectBase(
		UseValidationExceptions: UseValidationExceptions,
		ValueObjectType: ValueObjectType,
		UnderlyingTypeName: $"ImmutableDictionary<{KeyType.Name}, {ValueType.Name}{(AllowNull ? "?" : null)}>",
		UnderlyingTypeNameBase: $"Dictionary<{KeyType.Name}, {ValueType.Name}{(AllowNull ? "?" : null)}>",
		GenerateToString: GenerateToString, 
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		ConstructorIsPublic: ConstructorIsPublic,
		ForbidParameterlessConstruction: ForbidParameterlessConstruction, 
		GenerateStaticDefault: GenerateStaticDefault,
		GenerateEnumerable: GenerateEnumerable,
		PropertyName: PropertyName ?? "Value",
		PropertyIsPublic: PropertyIsPublic,
		AddIComparable: false,
		AllowNull: AllowNull),
		IEnumerableValueObject
{
	public string ElementTypeName { get; } = $"KeyValuePair<{KeyType.Name}, {ValueType.Name}{(AllowNull ? "?" : null)}>";
	public string ValueTypeName { get; } = $"{ValueType.Name}{(AllowNull ? "?" : null)}";
	
	public override string[] GetNamespaces()
	{
		var keyNamespace = this.KeyType.ContainingNamespace;
		var elementNamespace = this.ValueType.ContainingNamespace;

		if (!keyNamespace.IsGlobalNamespace && !elementNamespace.IsGlobalNamespace && !SymbolEqualityComparer.Default.Equals(keyNamespace, elementNamespace))
			return new[] { keyNamespace.ToDisplayString(), elementNamespace.ToDisplayString() };
		
		if (!keyNamespace.IsGlobalNamespace)
			return new[] { keyNamespace.ToDisplayString() };
		
		if (!elementNamespace.IsGlobalNamespace)
			return new[] { elementNamespace.ToDisplayString() };

		return Array.Empty<string>();
	}
	
	public override string GetCommentsCode()		=> $"An immutable value object with an immutable dictionary of {this.ValueTypeName} by {this.KeyType.Name} as underlying value.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ Key = \"{this.KeyType.Name}\", Value = \"{this.ValueTypeName}\" }}, extraText: this.Count.ToString());";
	
	public override string? GetInterfacesCode()		=> this.GenerateEnumerable ? $"IReadOnlyDictionary<{this.KeyType.Name}, {this.ValueType.Name}>" : null;

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.Count == 0 ? 1 : 2;";

	public override string GetEqualsCode()			=> $@"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other)
	{{
		if (ReferenceEquals(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName})) return true;
		if (other{this.NullOperator}.{this.PropertyName} is not {{ }} otherValue) return false;
		return this.{this.PropertyName}.SequenceEqual(otherValue);
	}}";
	public override string GetObjectEqualsCode()	=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string? GetCompareToCode()		=> null;

	public override string GetDefaultValue()		=> $"{this.UnderlyingTypeName}.Empty";
	
	public override string GetLengthOrCountCode()	=> $"public int Count => this.{this.PropertyName}.Count;";

	public override string GetExtraCastCode()		=> $"public static explicit operator {this.Name}({this.UnderlyingTypeNameBase} {this.LocalVariableName}) => new({this.LocalVariableName}.ToImmutableDictionary());";

	public override string GetValidationCode(string errorCodeStart)
	{
		var validation = new StringBuilder();

		validation.AppendLine(this.GetGuardLine(Guard.NotNull, null, errorCodeStart));

		if (this.MinimumCount is not null || this.MaximumCount is not null)
			validation.AppendLine(this.GetGuardLine<int>(Guard.InRange, $"{this.LocalVariableName}.Count", errorCodeStart, this.MinimumCount, this.MaximumCount));

		return validation.ToString();
	}

	public override string? GetValueTransformation() => null;

	public override string GetEnumeratorCode() => $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => this.{this.PropertyName}.GetEnumerator();";

	public override string GetExtraCode() => $@"
	public {(this.IsUnsealedRecordClass ? "virtual " : null)}{this.ValueTypeName} this[{this.KeyType.Name} key] 
		=> Validator<{this.Name}>.Default.GuardKeyExists(this.{this.PropertyName}.GetValueOrDefault, key, errorCode: null)!;

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<{this.KeyType.Name}> Keys => this.{this.PropertyName}.Keys;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<{this.ValueTypeName}> Values => this.{this.PropertyName}.Values;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ContainsKey({this.KeyType.Name} key) => this.{this.PropertyName}.ContainsKey(key);
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool TryGetValue({this.KeyType.Name} key, [MaybeNullWhen(false)] out {this.ValueTypeName} value) => this.{this.PropertyName}.TryGetValue(key, out value)!;
";
}
