namespace CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

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
	AlphaWithSpace,
	AlphaWithUnderscore,
	AlphaNumeric,
	AlphaNumericWithSpace,
	AlphaNumericWithUnderscore,
}

public sealed record StringValueObject(
		INamedTypeSymbol ValueObjectType,
		int? MinimumLength,
		int? MaximumLength,
		bool UseRegex,
		StringCaseConversion StringCaseConversion,
		StringFormat StringFormat,
		StringComparison StringComparison,
		bool GenerateEnumerable,
		bool GenerateToString,
		bool GenerateComparison,
		bool GenerateDefaultConstructor,
		bool ForbidParameterlessConstruction,
		bool GenerateStaticDefault,
		string? PropertyName,
		bool PropertyIsPublic,
		bool AllowNull,
		bool UseValidationExceptions)
	: ValueObjectBase(
		useValidationExceptions: UseValidationExceptions,
		valueObjectType: ValueObjectType,
		generateToString: GenerateToString,
		generateComparison: GenerateComparison,
		generateDefaultConstructor: GenerateDefaultConstructor,
		forbidParameterlessConstruction: ForbidParameterlessConstruction,
		generateStaticDefault: GenerateStaticDefault,
		generateEnumerable: GenerateEnumerable,
		propertyName: PropertyName ?? "Value",
		propertyIsPublic: PropertyIsPublic,
		addIComparable: true,
		allowNull: AllowNull,
		useCustomProperty: false),
		IEnumerableValueObject
{
	public string ElementTypeName					=> nameof(Char);

	public override string UnderlyingTypeName { get; } = AllowNull ? $"{nameof(String)}?" : nameof(String);
	public override string? UnderlyingTypeNameBase	=> null;

	public override IEnumerable<string> GetUsingNamespaces()
		=> Array.Empty<string>();

	public override string GetComments()				=> $"An immutable value type with a {(this.StringCaseConversion == StringCaseConversion.NoConversion ? null : $"{this.StringCaseConversion} ")}{this.StringFormat}-Formatted string as underlying value.";

	public override string GetToStringCode()			=> $"public override {this.UnderlyingTypeName} ToString() => this.{this.PropertyName};";

	public override string? GetInterfacesCode()			=> null;

	public override string GetHashCodeCode()			=> $"public override int GetHashCode() => String.GetHashCode(this.{this.PropertyName}, StringComparison.{this.StringComparison});";

	public override string GetEqualsCode()				=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.ValueObjectNullOperator} other) => String.Equals(this.{this.PropertyName}, other{this.ValueObjectNullOperator}.{this.PropertyName}, StringComparison.{this.StringComparison});";
	public override string GetObjectEqualsCode()		=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string GetCompareToCode()			=> $"public int CompareTo({this.Name}{this.ValueObjectNullOperator} other) => String.Compare(this.{this.PropertyName}, other{this.ValueObjectNullOperator}.{this.PropertyName}, StringComparison.{this.StringComparison});";

	public override string GetDefaultValue()			=> "String.Empty";

	public override string GetLengthOrCountCode()		=> this.AllowNull
		? $"public int Length => this.{this.PropertyName}?.Length ?? 0;"
		: $"public int Length => this.{this.PropertyName}.Length;";

	public override string? GetExtraCastCode()			=> null;

	public override string? GetExtraConstructorCode()	=> null;

	public override string GetValidationCode(string errorCodeStart)
	{
		var validation = new StringBuilder();

		if (!this.AllowNull)
			validation.AppendLine(this.GetGuardLine(Guard.NotNull, null, errorCodeStart));

		if (this.MinimumLength is not null || this.MaximumLength is not null)
			validation.AppendLine(this.GetGuardLine(Guard.LengthInRange, variableName: this.LocalVariableName, errorCodeStart, genericParameterName: null, this.MinimumLength, this.MaximumLength));

		if (this.StringFormat is not StringFormat.Default)
		{
			var formatRegex = this.StringFormat switch
			{
				StringFormat.Alpha						=> "^[a-zA-Z]+$",
				StringFormat.AlphaWithSpace				=> "^[a-zA-Z ]*$",
				StringFormat.AlphaWithUnderscore		=> "^[a-zA-Z_]+$",
				StringFormat.AlphaNumeric				=> "^[a-zA-Z0-9]+$",
				StringFormat.AlphaNumericWithSpace		=> "^[a-zA-Z0-9 ]*$",
				StringFormat.AlphaNumericWithUnderscore => "^[a-zA-Z0-9_]+$",
				_										=> throw new ArgumentOutOfRangeException(nameof(this.StringFormat), this.StringFormat, null)
			};

			validation.AppendLine(this.GetGuardLine(Guard.Regex, this.LocalVariableName, errorCodeStart, genericParameterName: null, $"new Regex(\"{formatRegex}\", RegexOptions.Compiled, matchTimeout: TimeSpan.FromSeconds(1))"));
		}

		if (this.UseRegex)
			validation.AppendLine(this.GetGuardLine(Guard.Regex, this.LocalVariableName, errorCodeStart, genericParameterName: null, "ValidationRegex()"));

		return validation.ToString();
	}

	public override string? GetValueTransformation()
		=> this.StringCaseConversion is not StringCaseConversion.NoConversion
			? $".To{this.StringCaseConversion}()"
			: null;

	public override string GetEnumeratorCode()
		=> this.AllowNull
			? $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => (this.{this.PropertyName} ?? String.Empty).GetEnumerator();"
			: $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => this.{this.PropertyName}.GetEnumerator();";

	public override string GetExtraCode()
		=> this.AllowNull
			? $@"
	public {(this.IsUnsealedRecordClass ? "virtual " : null)}{this.ElementTypeName}? this[int index] 
		=> this.{this.PropertyName} is null ? null : Validator.Get<{this.Name}>.Default.GuardIndexInRange(this.{this.PropertyName}, index, errorCode: null);
"
			: $@"
	public {(this.IsUnsealedRecordClass ? "virtual " : null)}{this.ElementTypeName} this[int index] 
		=> Validator.Get<{this.Name}>.Default.GuardIndexInRange(this.{this.PropertyName}, index, errorCode: null);
";
	}
