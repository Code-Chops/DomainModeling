namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record ListValueObject(
		bool UseValidationExceptions,
		int? MinimumCount,
		int? MaximumCount,
		INamedTypeSymbol ValueObjectType,
		// ReSharper disable once NotAccessedPositionalProperty.Global
		AttributeData Attribute,
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
		UnderlyingTypeName: $"ImmutableList<{Attribute.AttributeClass!.TypeArguments.Single().Name}{(AllowNull ? "?" : null)}>",
		UnderlyingTypeNameBase: $"List<{Attribute.AttributeClass!.TypeArguments.Single().Name}{(AllowNull ? "?" : null)}>",
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
	public string ElementTypeName { get; } = $"{Attribute.AttributeClass!.TypeArguments.Single().Name}{(AllowNull ? "?" : null)}";

	public override string[] GetNamespaces()
	{
		var elementNamespace = this.Attribute.AttributeClass!.TypeArguments.Single().ContainingNamespace;
		if (elementNamespace.IsGlobalNamespace) return Array.Empty<string>();

		return new[] { elementNamespace.ToDisplayString() };
	}

	public override string GetCommentsCode()		=> $"An immutable value object with an immutable list of {this.ElementTypeName} as underlying value.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ Type = \"{this.ElementTypeName}\" }}, extraText: this.Count.ToString());";
	
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

	public override string GetDefaultValue()		=> $"ImmutableList<{this.ElementTypeName}>.Empty";
	
	public override string GetLengthOrCountCode()	=> $"public int Count => this.{this.PropertyName}.Count;";

	public override string GetExtraCastCode()		=> $"public static explicit operator {this.Name}({this.UnderlyingTypeNameBase} {this.LocalVariableName}) => new({this.LocalVariableName}.ToImmutableList());";

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
	public {(this.IsUnsealedRecordClass ?  "virtual " : null)}{this.ElementTypeName} this[int index] 
		=> Validator.Get<{this.Name}>.Default.GuardInRange(this.{this.PropertyName}, index, errorCode: null)!;";
}
