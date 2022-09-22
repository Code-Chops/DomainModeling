namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record DictionaryValueObject(
		INamedTypeSymbol ValueObjectType,
		ITypeSymbol KeyType,
		ITypeSymbol ValueType,
		TypeDeclarationSyntax TypeDeclarationSyntax,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool GenerateDefaultConstructor,
		bool GenerateParameterlessConstructor,
		bool GenerateEmptyStatic,
		bool GenerateEnumerable,
		string? PropertyName,
		bool PropertyIsPublic,
		int? MinimumCount,
		int? MaximumCount) 
	: ValueObjectBase(
		ValueObjectType: ValueObjectType,
		TypeDeclarationSyntax: TypeDeclarationSyntax,
		UnderlyingTypeName: $"ImmutableDictionary<{KeyType},{ValueType}>",
		UnderlyingTypeNameBase: $"Dictionary<{KeyType},{ValueType}>",
		GenerateToString: GenerateToString, 
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		GenerateParameterlessConstructor: GenerateParameterlessConstructor, 
		GenerateEmptyStatic: GenerateEmptyStatic,
		GenerateEnumerable: GenerateEnumerable,
		PropertyName: PropertyName ?? "Dictionary",
		PropertyIsPublic: PropertyIsPublic,
		AddIComparable: false),
		IEnumerableValueObject
{
	public string ElementTypeName { get; } = $"KeyValuePair<{KeyType.Name}, {ValueType.Name}>";
	
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
	
	public override string GetCommentsCode()		=> $"A dictionary of {this.ValueType.Name} by {this.KeyType.Name}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ Key = \"{this.KeyType.Name}\", Value = \"{this.ValueType.Name}\" }}, this.Count.ToString());";
	
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

	public override string GetDefaultValue()		=> $"new Dictionary<{this.KeyType.Name}, {this.ValueType.Name}>().ToImmutableDictionary()";
	
	public override string GetLengthOrCountCode()	=> $"public int Count => this.{this.PropertyName}.Count;";

	public override string GetExtraCastCode()		=> $"	public static explicit operator {this.Name}({this.UnderlyingTypeNameBase} {this.LocalVariableName}) => new({this.LocalVariableName}.ToImmutableDictionary());";

	public override string GetValidationCode()
	{
		var validation = new StringBuilder();

		validation.AppendLine($@"			if (value is null) throw new ArgumentNullException(nameof(value));");

		if (this.MinimumCount is not null)
			validation.AppendLine($@"			if (value.Count < {this.MinimumCount}) throw new ArgumentException($""Count {{value.Count}} of {this.Name} is less than {nameof(this.MinimumCount)} {this.MinimumCount}."");");
			
		if (this.MaximumCount is not null)
			validation.AppendLine($@"			if (value.Count > {this.MaximumCount}) throw new ArgumentException($""Count {{value.Count}} of {this.Name} is higher than {nameof(this.MaximumCount)} {this.MaximumCount}."");");

		return validation.ToString();
	}
	
	public override string GetEnumeratorCode() => $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => this.{this.PropertyName}.GetEnumerator();";

	public override string GetExtraCode() => $@"public {(this.IsUnsealedRecordClass ? "virtual " : null)}{this.ValueType.Name} this[{this.KeyType.Name} key] => this.{this.PropertyName}.TryGetValue(key, out var value) ? value : throw KeyNotFoundException<{this.KeyType.Name}, {this.Name}>.Create(key);
	public IEnumerable<{this.KeyType.Name}> Keys => this.{this.PropertyName}.Keys;
	public IEnumerable<{this.ValueType.Name}> Values => this.{this.PropertyName}.Values;
	public bool ContainsKey({this.KeyType.Name} key) => this.{this.PropertyName}.ContainsKey(key);
	public bool TryGetValue({this.KeyType.Name} key, [MaybeNullWhen(false)] out {this.ValueType.Name} value) => this.{this.PropertyName}.TryGetValue(key, out value);
";
}