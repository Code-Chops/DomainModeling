namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record DictionaryValueObject(
		INamedTypeSymbol Type,
		AttributeData Attribute,
		string Declaration,
		bool GenerateToString,
		bool AddCustomValidation,
		bool GenerateDefaultConstructor,
		bool GenerateParameterlessConstructor,
		bool GenerateEmptyStatic,
		string? PropertyName,
		int? MinimumCount,
		int? MaximumCount) 
	: ValueObjectBase(
		Type: Type,
		Declaration: Declaration,
		TypeName: $"ImmutableDictionary<{Attribute.AttributeClass!.TypeArguments[0].Name},{Attribute.AttributeClass!.TypeArguments[1].Name}>",
		ElementTypeName: Attribute.AttributeClass!.TypeArguments[1].Name,
		GenerateToString: GenerateToString, 
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		GenerateParameterlessConstructor: GenerateParameterlessConstructor, 
		GenerateEmptyStatic: GenerateEmptyStatic,
		PropertyName: PropertyName ?? "Dictionary",
		GenerateComparable: false)
{
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

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToEasyString(new {{ Key = \"{this.KeyTypeName}\", Value = \"{this.ElementTypeName}\" }});";
	
	public override string GetInterfacesCode()		=> $"IReadOnlyDictionary<{this.KeyTypeName}, {this.ElementTypeName}>";

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.Count == 0 ? 1 : 2;";

	public override string GetEqualsCode()			=> $@"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}? other)
	{{
		if (ReferenceEquals(this.{this.PropertyName}, other?.{this.PropertyName})) return true;
		if (other?.{this.PropertyName} is not {{ }} otherValue) return false;
		return this.{this.PropertyName}.SequenceEqual(otherValue);
	}}";
	
	public override string? GetCompareToCode()		=> null;

	public override string GetDefaultValue()		=> $"new Dictionary<{this.KeyTypeName}, {this.ElementTypeName}>().ToImmutableDictionary()";
	
	public override string GetLengthOrCountCode()	=> $"public int Count => this.{this.PropertyName}.Count;";

	public override string GetValidationCode()
	{
		var validation = new StringBuilder();

		if (this.MinimumCount is not null)
			validation.AppendLine($@"			if (value.Count < {this.MinimumCount}) throw new ArgumentException($""Count of {this.Name} is less ({{value.Count}}) than {nameof(this.MinimumCount)} {this.MinimumCount}."");");
			
		if (this.MaximumCount is not null)
			validation.AppendLine($@"			if (value.Count > {this.MaximumCount}) throw new ArgumentException($""Count of {this.Name} is higher ({{value.Count}}) than {nameof(this.MaximumCount)} {this.MaximumCount}."");");

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