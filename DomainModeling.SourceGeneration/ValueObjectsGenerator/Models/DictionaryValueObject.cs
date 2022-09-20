namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record DictionaryValueObject(
		INamedTypeSymbol ValueObjectType,
		AttributeData Attribute,
		string Declaration,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool GenerateDefaultConstructor,
		bool GenerateParameterlessConstructor,
		bool GenerateEmptyStatic,
		string? PropertyName,
		int? MinimumCount,
		int? MaximumCount) 
	: ValueObjectBase(
		ValueObjectType: ValueObjectType,
		Declaration: Declaration,
		UnderlyingTypeName: $"ImmutableDictionary<{Attribute.AttributeClass!.TypeArguments[0].Name},{Attribute.AttributeClass!.TypeArguments[1].Name}>",
		UnderlyingTypeNameBase: $"Dictionary<{Attribute.AttributeClass!.TypeArguments[0].Name},{Attribute.AttributeClass!.TypeArguments[1].Name}>",
		GenerateToString: GenerateToString, 
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		GenerateParameterlessConstructor: GenerateParameterlessConstructor, 
		GenerateEmptyStatic: GenerateEmptyStatic,
		PropertyName: PropertyName ?? "Dictionary",
		AddIComparable: false)
{
	public string ElementTypeName { get; } = Attribute.AttributeClass!.TypeArguments[1].Name;

	
	public override string[] GetNamespaces()
	{
		var keyNamespace = this.Attribute.AttributeClass!.TypeArguments[0].ContainingNamespace;
		var elementNamespace = this.Attribute.AttributeClass!.TypeArguments[1].ContainingNamespace;

		if (!keyNamespace.IsGlobalNamespace && !elementNamespace.IsGlobalNamespace && !SymbolEqualityComparer.Default.Equals(keyNamespace, elementNamespace))
			return new[] { keyNamespace.ToDisplayString(), elementNamespace.ToDisplayString() };
		
		if (!keyNamespace.IsGlobalNamespace)
			return new[] { keyNamespace.ToDisplayString() };
		
		if (!elementNamespace.IsGlobalNamespace)
			return new[] { elementNamespace.ToDisplayString() };

		return Array.Empty<string>();
	}
	
	public string KeyTypeName { get; } = Attribute.AttributeClass!.TypeArguments[0].Name;
	
	public override string GetCommentsCode()		=> $"A dictionary of {this.ElementTypeName} by {this.KeyTypeName}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ Key = \"{this.KeyTypeName}\", Value = \"{this.ElementTypeName}\" }}, this.Count.ToString());";
	
	public override string GetInterfacesCode()		=> $"IReadOnlyDictionary<{this.KeyTypeName}, {this.ElementTypeName}>";

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.Count == 0 ? 1 : 2;";

	public override string GetEqualsCode()			=> $@"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other)
	{{
		if (ReferenceEquals(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName})) return true;
		if (other{this.NullOperator}.{this.PropertyName} is not {{ }} otherValue) return false;
		return this.{this.PropertyName}.SequenceEqual(otherValue);
	}}";
	public override string GetObjectEqualsCode()	=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string? GetCompareToCode()		=> null;

	public override string GetDefaultValue()		=> $"new Dictionary<{this.KeyTypeName}, {this.ElementTypeName}>().ToImmutableDictionary()";
	
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
	
	public override string GetEnumeratorCode() => $"public IEnumerator<KeyValuePair<{this.KeyTypeName}, {this.ElementTypeName}>> GetEnumerator() => this.{this.PropertyName}.GetEnumerator();";

	public override string GetExtraCode() => $@"public {(this.IsUnsealedRecordClass ? "virtual " : null)}{this.ElementTypeName} this[{this.KeyTypeName} key] => this.{this.PropertyName}.TryGetValue(key, out var value) ? value : throw KeyNotFoundException<{this.KeyTypeName}, {this.Name}>.Create(key);
	public IEnumerable<{this.KeyTypeName}> Keys => this.{this.PropertyName}.Keys;
	public IEnumerable<{this.ElementTypeName}> Values => this.{this.PropertyName}.Values;
	public bool ContainsKey({this.KeyTypeName} key) => this.{this.PropertyName}.ContainsKey(key);
	public bool TryGetValue({this.KeyTypeName} key, [MaybeNullWhen(false)] out {this.ElementTypeName} value) => this.{this.PropertyName}.TryGetValue(key, out value);
";
}