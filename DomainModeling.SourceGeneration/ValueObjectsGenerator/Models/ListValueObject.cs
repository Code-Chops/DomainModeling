namespace CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public record ListValueObject : ValueObjectBase, IEnumerableValueObject
{
	public ListValueObject(
		INamedTypeSymbol valueObjectType,
		ITypeSymbol? providedElementType,
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
			allowNull: allowNull,
			useValidationExceptions: useValidationExceptions)
	{
		providedElementType = GetElementType(valueObjectType, providedElementType);
		
		if (providedElementType is null)
		{
			this.ErrorMessage = "Underlying type for element unknown. No underlying type provided as attribute type argument, or as type parameter on the type.";
			return;
		}
		
		this.ProvidedElementType = providedElementType;
		this.MinimumCount = minimumCount;
		this.MaximumCount = maximumCount;
		this.ElementTypeName = $"{providedElementType.Name}{(allowNull ? "?" : null)}";
		this.UnderlyingTypeName = $"ImmutableList<{providedElementType.Name}{(allowNull ? "?" : null)}>";
		this.UnderlyingTypeNameBase = $"List<{providedElementType.Name}{(allowNull ? "?" : null)}>";
	}
	
	private static ITypeSymbol? GetElementType(INamedTypeSymbol valueObjectType, ITypeSymbol? providedElementType)
	{
		return providedElementType ?? valueObjectType.TypeArguments.FirstOrDefault();
	}

	public string ElementTypeName { get; } = null!;

	public override string UnderlyingTypeName { get; } = null!;
	public override string? UnderlyingTypeNameBase { get; }
	public ITypeSymbol ProvidedElementType { get; } = null!;
	public int? MinimumCount { get; }
	public int? MaximumCount { get; }

	public override string[] GetNamespaces()
	{
		var elementNamespace = this.ProvidedElementType.ContainingNamespace;
		if (elementNamespace.IsGlobalNamespace) 
			return Array.Empty<string>();

		return new[] { elementNamespace.ToDisplayString() };
	}

	public override string GetComments()
	{
		var attribute = this.ValueObjectType.IsGenericType
			? "typeparamref name"
			: "see cref";
		
		return $@"An immutable value object with an immutable list of <{attribute}=""{this.ProvidedElementType.GetTypeNameWithGenericParameters().Replace('<', '{').Replace('>', '}')}""/> as underlying value.";
	}

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

	public override string GetExtraCastCode()		=> $"public static explicit operator {this.Name}({this.UnderlyingTypeNameBase ?? this.UnderlyingTypeName} {this.LocalVariableName}) => new({this.LocalVariableName}.ToImmutableList());";

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
		=> Validator.Get<{this.Name}>.Default.GuardIndexInRange(this.{this.PropertyName}, index, errorCode: null)!;";
}
