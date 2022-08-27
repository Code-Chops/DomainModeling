namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record ListValueObject(
		INamedTypeSymbol Type,
		// ReSharper disable once NotAccessedPositionalProperty.Global
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
		TypeName: $"ImmutableList<{Attribute.AttributeClass!.TypeArguments.Single().Name}>",
		ElementTypeName: Attribute.AttributeClass!.TypeArguments.Single().Name,
		GenerateToString: GenerateToString,
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		GenerateParameterlessConstructor: GenerateParameterlessConstructor, 
		GenerateEmptyStatic: GenerateEmptyStatic,
		PropertyName: PropertyName ?? "List",
		GenerateComparable: false)
{
	public override string[] GetNamespaces()
	{
		var elementNamespace = this.Attribute.AttributeClass!.TypeArguments.Single().ContainingNamespace;
		if (elementNamespace.IsGlobalNamespace) return Array.Empty<string>();

		return new[] { elementNamespace.ToDisplayString() };
	}

	public override string GetCommentsCode()		=> $"An enumerable of {this.ElementTypeName}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToEasyString(null);";
	
	public override string GetInterfacesCode()		=> $"IEnumerable<{this.ElementTypeName}>";

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.Count == 0 ? 1 : 2;";

	public override string GetEqualsCode()			=> $@"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}? other)
	{{
		if (ReferenceEquals(this.{this.PropertyName}, other?.{this.PropertyName})) return true;
		if (other?.{this.PropertyName} is not {{ }} otherValue) return false;
		return this.{this.PropertyName}.SequenceEqual(otherValue);
	}}";
	
	public override string? GetCompareToCode()		=> null;

	public override string GetDefaultValue()		=> $"new List<{this.ElementTypeName}>().ToImmutableList()";
	
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
	
	public override string GetEnumeratorCode() => $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => this.{this.PropertyName}.GetEnumerator();";

	public override string GetExtraCode() => $@"public {(this.IsUnsealedRecordClass ?  "virtual " : null)}{this.ElementTypeName} this[int index] => index < this.Count ? this.{this.PropertyName}.ElementAt(index) : throw IndexOutOfRangeException<{this.Name}>.Create(index);";
}