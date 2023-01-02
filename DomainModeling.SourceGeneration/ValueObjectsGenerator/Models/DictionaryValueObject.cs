namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record DictionaryValueObject : ValueObjectBase, IEnumerableValueObject
{
	public DictionaryValueObject(
		INamedTypeSymbol valueObjectType,
		ITypeSymbol keyType,
		ITypeSymbol valueType,
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
		keyType = GetUnderlyingType(valueObjectType, keyType, isKey: true);
		valueType = GetUnderlyingType(valueObjectType, valueType, isKey: false);
		
		this.KeyType = keyType;
		this.ValueType = valueType;
		this.MinimumCount = minimumCount;
		this.MaximumCount = maximumCount;
		this.ElementTypeName = $"KeyValuePair<{keyType.Name}, {valueType.Name}{(allowNull ? "?" : null)}>";
		this.ValueTypeName = $"{valueType.Name}{(allowNull ? "?" : null)}";
		this.UnderlyingTypeName = $"ImmutableDictionary<{keyType.Name}, {valueType.Name}{(allowNull ? "?" : null)}>";
		this.UnderlyingTypeNameBase = $"Dictionary<{keyType.Name}, {valueType.Name}{(allowNull ? "?" : null)}>";
	}

	private static ITypeSymbol GetUnderlyingType(INamedTypeSymbol valueObjectType, ITypeSymbol underlyingType, bool isKey)
	{
		var index = isKey 
			? (valueObjectType.TypeArguments.Length == 1 ? 1 : 0)
			: (valueObjectType.TypeArguments.Length == 1 ? 0 : 1); 
		
		var typeParameter = valueObjectType.TypeArguments
			.OfType<ITypeSymbol>()
			.Skip(index)
			.FirstOrDefault();
		
		return typeParameter ?? underlyingType;
	}

	public string ElementTypeName { get; }
	public string ValueTypeName { get; }

	public override string UnderlyingTypeName { get; }
	public override string? UnderlyingTypeNameBase { get; }
	public ITypeSymbol KeyType { get; }
	public ITypeSymbol ValueType { get; }
	public int? MinimumCount { get; }
	public int? MaximumCount { get; }

	public override string[] GetNamespaces()
	{
		var keyNamespace = this.KeyType.ContainingNamespace;
		var elementNamespace = this.ValueType.ContainingNamespace;

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

		return $@"An immutable value object holding an immutable dictionary with <{attributeKey}=""{this.KeyType.GetFullTypeNameWithGenericParameters().Replace('<', '{').Replace('>', '}')}""/> as key and <{attributeValue}=""{this.ValueType.GetFullTypeNameWithGenericParameters().Replace('<', '{').Replace('>', '}')}""/> as value.";
	}

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
	public {(this.IsUnsealedRecordClass ? "virtual " : null)}{this.ValueTypeName} this[{this.KeyType.Name} key] 
		=> Validator.Get<{this.Name}>.Default.GuardKeyExists(this.{this.PropertyName}.GetValueOrDefault, key, errorCode: null)!;

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
