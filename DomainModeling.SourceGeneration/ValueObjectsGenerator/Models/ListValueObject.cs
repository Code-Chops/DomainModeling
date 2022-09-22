namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record ListValueObject(
		INamedTypeSymbol ValueObjectType,
		// ReSharper disable once NotAccessedPositionalProperty.Global
		AttributeData Attribute,
		TypeDeclarationSyntax TypeDeclarationSyntax,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool GenerateDefaultConstructor,
		bool GenerateParameterlessConstructor,
		bool GenerateEmptyStatic,
		bool GenerateEnumerable,
		string? PropertyName,
		int? MinimumCount,
		int? MaximumCount) 
	: ValueObjectBase(
		ValueObjectType: ValueObjectType,
		TypeDeclarationSyntax: TypeDeclarationSyntax,
		UnderlyingTypeName: $"ImmutableList<{Attribute.AttributeClass!.TypeArguments.Single().Name}>",
		UnderlyingTypeNameBase: $"List<{Attribute.AttributeClass!.TypeArguments.Single().Name}>",
		GenerateToString: GenerateToString,
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		GenerateParameterlessConstructor: GenerateParameterlessConstructor, 
		GenerateEmptyStatic: GenerateEmptyStatic,
		GenerateEnumerable: GenerateEnumerable,
		PropertyName: PropertyName ?? "List",
		AddIComparable: false), 
		IEnumerableValueObject
{
	public string ElementTypeName { get; } = Attribute.AttributeClass!.TypeArguments.Single().Name;

	public override string[] GetNamespaces()
	{
		var elementNamespace = this.Attribute.AttributeClass!.TypeArguments.Single().ContainingNamespace;
		if (elementNamespace.IsGlobalNamespace) return Array.Empty<string>();

		return new[] { elementNamespace.ToDisplayString() };
	}

	public override string GetCommentsCode()		=> $"An enumerable of {this.ElementTypeName}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ Type = typeof({this.ElementTypeName}).Name }}, this.Count.ToString());";
	
	public override string? GetInterfacesCode()		=> this.GenerateEnumerable ? $"IReadOnlyList<{this.ElementTypeName}>" : null;

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.Count == 0 ? 1 : 2;";

	public override string GetEqualsCode()			=> $@"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other)
	{{
		if (ReferenceEquals(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName})) return true;
		if (other{this.NullOperator}.{this.PropertyName} is not {{ }} otherValue) return false;
		return this.{this.PropertyName}.SequenceEqual(otherValue);
	}}";
	public override string GetObjectEqualsCode()	=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string? GetCompareToCode()		=> null;

	public override string GetDefaultValue()		=> $"new List<{this.ElementTypeName}>().ToImmutableList()";
	
	public override string GetLengthOrCountCode()	=> $"public int Count => this.{this.PropertyName}.Count;";

	public override string GetExtraCastCode()		=> $"	public static explicit operator {this.Name}({this.UnderlyingTypeNameBase} {this.LocalVariableName}) => new({this.LocalVariableName}.ToImmutableList());";

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

	public override string GetExtraCode() => $@"public {(this.IsUnsealedRecordClass ?  "virtual " : null)}{this.ElementTypeName} this[int index] => index >= 0 && index < this.Count ? this.{this.PropertyName}[index] : throw IndexOutOfRangeException<{this.Name}>.Create(index);";
}