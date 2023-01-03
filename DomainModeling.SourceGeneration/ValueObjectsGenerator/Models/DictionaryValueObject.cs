namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record DictionaryValueObject : ValueObjectBase, IEnumerableValueObject
{
	public string ElementTypeName { get; } = null!;
	public string ValueTypeName { get; } = null!;

	public override string UnderlyingTypeName { get; } = null!;
	public override string? UnderlyingTypeNameBase { get; }
	public ITypeSymbol ProvidedKeyType { get; } = null!;
	public ITypeSymbol ProvidedValueType { get; } = null!;
	public int? MinimumCount { get; }
	public int? MaximumCount { get; }
	
	public DictionaryValueObject(
		INamedTypeSymbol valueObjectType,
		ITypeSymbol? providedKeyType,
		ITypeSymbol? providedValueType,
		int? minimumCount,
		int? maximumCount,
		bool generateEnumerable,
		bool generateToString,
		bool generateComparison,
		bool generateDefaultConstructor,
		bool forbidParameterlessConstruction,
		bool generateStaticDefault,
		string? propertyName,
		bool propertyIsPublic,
		bool allowNull,
		bool useValidationExceptions) 
		: base(
			useValidationExceptions: useValidationExceptions,
			valueObjectType: valueObjectType,
			generateToString: generateToString, 
			generateComparison: generateComparison,
			generateDefaultConstructor: generateDefaultConstructor,
			forbidParameterlessConstruction: forbidParameterlessConstruction, 
			generateStaticDefault: generateStaticDefault,
			generateEnumerable: generateEnumerable,
			propertyName: propertyName ?? "Value",
			propertyIsPublic: propertyIsPublic,
			addIComparable: false,
			allowNull: allowNull)
	{
		providedKeyType = GetUnderlyingType(valueObjectType, providedKeyType, isKey: true);
		providedValueType = GetUnderlyingType(valueObjectType, providedValueType, isKey: false);
		
		if (providedKeyType is null)
		{
			this.ErrorMessage = "Underlying type for key unknown. No underlying type provided as attribute type argument, or as type parameter on the type.";
			return;
		}
		if (providedValueType is null)
		{
			this.ErrorMessage = "Underlying type for value unknown. No underlying type provided as attribute type argument, or as type parameter on the type.";
			return;
		}
		
		this.ProvidedKeyType = providedKeyType;
		this.ProvidedValueType = providedValueType;
		this.MinimumCount = minimumCount;
		this.MaximumCount = maximumCount;
		this.ElementTypeName = $"KeyValuePair<{providedKeyType.Name}, {providedValueType.Name}{(allowNull ? "?" : null)}>";
		this.ValueTypeName = $"{providedValueType.Name}{(allowNull ? "?" : null)}";
		this.UnderlyingTypeName = $"ImmutableDictionary<{providedKeyType.Name}, {providedValueType.Name}{(allowNull ? "?" : null)}>";
		this.UnderlyingTypeNameBase = $"Dictionary<{providedKeyType.Name}, {providedValueType.Name}{(allowNull ? "?" : null)}>";
	}

	private static ITypeSymbol? GetUnderlyingType(INamedTypeSymbol valueObjectType, ITypeSymbol? providedUnderlyingType, bool isKey)
	{
		if (providedUnderlyingType is not null)
			return providedUnderlyingType;
		
		var index = isKey 
			? (valueObjectType.TypeArguments.Length == 1 ? 1 : 0)
			: (valueObjectType.TypeArguments.Length == 1 ? 0 : 1); 
		
		var typeParameter = valueObjectType.TypeArguments
			.OfType<ITypeSymbol>()
			.Skip(index)
			.FirstOrDefault();
		
		return typeParameter ?? providedUnderlyingType;
	}

	public override string[] GetNamespaces()
	{
		var keyNamespace = this.ProvidedKeyType.ContainingNamespace;
		var elementNamespace = this.ProvidedValueType.ContainingNamespace;

		if (!keyNamespace.IsGlobalNamespace)
			return new[] { keyNamespace.ToDisplayString() };
		
		if (!elementNamespace.IsGlobalNamespace)
			return new[] { elementNamespace.ToDisplayString() };

		return Array.Empty<string>();
	}
	
	public override string GetComments()
	{
		var attributeKey = this.ValueObjectType.TypeArguments.Length != 1
			? "typeparamref name"
			: "see cref";
		
		var attributeValue = this.ValueObjectType.TypeArguments.Length > 0
			? "typeparamref name"
			: "see cref";

		return $@"An immutable value object holding an immutable dictionary with <{attributeKey}=""{this.ProvidedKeyType.GetTypeNameWithGenericParameters().Replace('<', '{').Replace('>', '}')}""/> as key and <{attributeValue}=""{this.ProvidedValueType.GetTypeNameWithGenericParameters().Replace('<', '{').Replace('>', '}')}""/> as value.";
	}

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ Key = \"{this.ProvidedKeyType.Name}\", Value = \"{this.ValueTypeName}\" }}, extraText: this.Count.ToString());";
	
	public override string? GetInterfacesCode()		=> this.GenerateEnumerable ? $"IReadOnlyDictionary<{this.ProvidedKeyType.Name}, {this.ProvidedValueType.Name}>" : null;

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

	public override string GetExtraCastCode()		=> $"public static explicit operator {this.Name}({this.UnderlyingTypeNameBase ?? this.UnderlyingTypeName} {this.LocalVariableName}) => new({this.LocalVariableName}.ToImmutableDictionary());";

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
	public {(this.IsUnsealedRecordClass ? "virtual " : null)}{this.ValueTypeName} this[{this.ProvidedKeyType.Name} key] 
		=> Validator.Get<{this.Name}>.Default.GuardKeyExists(this.{this.PropertyName}.GetValueOrDefault, key, errorCode: null)!;

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<{this.ProvidedKeyType.Name}> Keys => this.{this.PropertyName}.Keys;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerable<{this.ValueTypeName}> Values => this.{this.PropertyName}.Values;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ContainsKey({this.ProvidedKeyType.Name} key) => this.{this.PropertyName}.ContainsKey(key);
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool TryGetValue({this.ProvidedKeyType.Name} key, [MaybeNullWhen(false)] out {this.ValueTypeName} value) => this.{this.PropertyName}.TryGetValue(key, out value)!;
";
}
