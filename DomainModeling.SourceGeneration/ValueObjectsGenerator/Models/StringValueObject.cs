namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

/// <summary>
/// DO NOT RENAME!
/// </summary>
public enum StringCaseConversion
{
	NoConversion,
	LowerInvariant,
	UpperInvariant,
}

public enum StringFormat
{
	Default,
	Alpha,
	AlphaWithUnderscore,
	AlphaNumeric,
	AlphaNumericWithUnderscore,
}

public record StringValueObject(
		bool UseValidationExceptions,
		int? MinimumLength,
		int? MaximumLength,
		INamedTypeSymbol ValueObjectType,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool ConstructorIsPublic,
		bool ForbidParameterlessConstruction,
		bool GenerateStaticDefault,
		bool GenerateEnumerable,
		string? PropertyName,
		bool PropertyIsPublic,
		bool AllowNull,
		StringCaseConversion StringCaseConversion,
		StringFormat StringFormat,
		StringComparison StringComparison) 
	: ValueObjectBase(
		UseValidationExceptions: UseValidationExceptions,
		ValueObjectType: ValueObjectType,
		UnderlyingTypeName: nameof(String),
		UnderlyingTypeNameBase: null,
		GenerateToString: GenerateToString,
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		ConstructorIsPublic: ConstructorIsPublic,
		ForbidParameterlessConstruction: ForbidParameterlessConstruction,
		GenerateStaticDefault: GenerateStaticDefault,
		GenerateEnumerable: GenerateEnumerable,
		PropertyName: PropertyName ?? "Value",
		PropertyIsPublic: PropertyIsPublic,
		AddIComparable: true,
		AllowNull: AllowNull),
		IEnumerableValueObject
{
	public string ElementTypeName => nameof(Char);

	public override string[] GetNamespaces()		=> Array.Empty<string>();
	
	public override string GetCommentsCode()		=> $"An immutable value type with an underlying value of {(this.StringCaseConversion == StringCaseConversion.NoConversion ? null : $"{this.StringCaseConversion} ")}{this.StringFormat} string.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ this.{this.PropertyName} }});";
	
	public override string? GetInterfacesCode()		=> null;

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => String.GetHashCode({this.PropertyName}, StringComparison.{this.StringComparison});";

	public override string GetEqualsCode()			=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other) => String.Equals(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName}, StringComparison.{this.StringComparison});";
	public override string GetObjectEqualsCode()	=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string GetCompareToCode()		=> $"public int CompareTo({this.Name}{this.NullOperator} other) => String.Compare(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName}, StringComparison.{this.StringComparison});";

	public override string GetDefaultValue()		=> $"String.Empty";
	
	public override string GetLengthOrCountCode()	=> $"public int Length => this.{this.PropertyName}.Length;";

	public override string? GetExtraCastCode()		=> null;
	
	public override string GetValidationCode(string errorCodeStart)
	{
		var validation = new StringBuilder();

		if (!this.AllowNull)
			validation.AppendLine(this.GetGuardLine(Guard.NotNull, null, errorCodeStart));

		if (this.StringFormat is not StringFormat.Default)
		{
			var formatRegex = this.StringFormat switch
			{
				StringFormat.Alpha						=> "^[a-zA-Z]+$",
				StringFormat.AlphaWithUnderscore		=> "^[a-zA-Z_]+$",
				StringFormat.AlphaNumeric				=> "^[a-zA-Z0-9]+$",
				StringFormat.AlphaNumericWithUnderscore => "^[a-zA-Z0-9_]+$",
				_										=> throw new ArgumentOutOfRangeException(nameof(this.StringFormat), this.StringFormat, null)
			};

			validation.AppendLine(this.GetGuardLine(Guard.Regex, this.LocalVariableName, errorCodeStart, genericParameterName: null, $"\"{formatRegex}\""));
		}
			
		if (this.MinimumLength is not null || this.MaximumLength is not null)
			validation.AppendLine(this.GetGuardLine<int>(Guard.InRange, valueName: $"{this.LocalVariableName}.Length", errorCodeStart, this.MinimumLength, this.MaximumLength));
		
		return validation.ToString();
	}
	
	public override string? GetValueTransformation() 
		=> this.StringCaseConversion is not StringCaseConversion.NoConversion 
			? $".To{this.StringCaseConversion}()"
			: null;

	public override string GetEnumeratorCode() => $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => this.{this.PropertyName}.GetEnumerator();";

	public override string GetExtraCode()	=> $@"public {(this.IsUnsealedRecordClass ?  "virtual " : null)}{this.ElementTypeName}? this[int index] 
		=> Validator.Get<{this.Name}>.Default.GuardInRange(this.{this.PropertyName}, index, errorCode: null)!;";
}
