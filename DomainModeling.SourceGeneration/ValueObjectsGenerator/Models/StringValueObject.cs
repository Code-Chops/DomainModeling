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
		INamedTypeSymbol ValueObjectType,
		string Declaration,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool GenerateDefaultConstructor,
		bool GenerateParameterlessConstructor,
		bool GenerateEmptyStatic,
		string? PropertyName,
		bool AllowNull,
		int? MinimumLength,
		int? MaximumLength,
		StringCaseConversion StringCaseConversion,
		StringFormat StringFormat,
		StringComparison CompareOptions) 
	: ValueObjectBase(
		ValueObjectType: ValueObjectType,
		Declaration: Declaration,
		UnderlyingTypeName: nameof(String),
		UnderlyingTypeNameBase: null,
		GenerateToString: GenerateToString,
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		GenerateParameterlessConstructor: GenerateParameterlessConstructor,
		GenerateEmptyStatic: GenerateEmptyStatic,
		PropertyName: PropertyName ?? "Value",
		AddIComparable: true)
{
	public string ElementTypeName => nameof(Char);

	
	public override string[] GetNamespaces()		=> Array.Empty<string>();
	
	public override string GetCommentsCode()		=> $"A {(this.StringCaseConversion == StringCaseConversion.NoConversion ? null : $"{this.StringCaseConversion} ")}{this.StringFormat} string.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToEasyString(new {{ this.{this.PropertyName} }});";
	
	public override string GetInterfacesCode()		=> $"IEnumerable<{this.ElementTypeName}>";

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => String.GetHashCode({this.PropertyName}, StringComparison.{this.CompareOptions});";

	public override string GetEqualsCode()			=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other) => String.Equals(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName}, StringComparison.{this.CompareOptions});";
	public override string GetObjectEqualsCode()	=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string GetCompareToCode()		=> $"public int CompareTo({this.Name}{this.NullOperator} other) => String.Compare(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName}, StringComparison.{this.CompareOptions});";

	public override string GetDefaultValue()		=> $"\"\"";
	
	public override string GetLengthOrCountCode()	=> $"public int Length => this.{this.PropertyName}.Length;";

	public override string? GetExtraCastCode()		=> null;
	
	public override string GetValidationCode()
	{
		var validation = new StringBuilder();

		if (!this.AllowNull)
			validation.AppendLine($@"			if (value is null) throw new ArgumentNullException(nameof(value));");

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

			validation.AppendLine($@"			if (Regex.IsMatch(value, ""{formatRegex}"", RegexOptions.Compiled)) throw new ArgumentException(""Invalid characters in {this.Name} of format {this.StringFormat}. {this.PropertyName} '{{value}}'."");");
		}
			
		if (this.MinimumLength is not null)
			validation.AppendLine($@"			if (value.Length < {this.MinimumLength}) throw new ArgumentException($""String of {this.Name} is shorter ({{value.Length}} characters) than {nameof(this.MinimumLength)} {this.MinimumLength}."");");
			
		if (this.MaximumLength is not null)
			validation.AppendLine($@"			if (value.Length > {this.MaximumLength}) throw new ArgumentException($""String of {this.Name} is longer ({{value.Length}} characters) than {nameof(this.MaximumLength)} {this.MaximumLength}."");");

		if (this.StringCaseConversion is not StringCaseConversion.NoConversion)
			validation.AppendLine($"			value = value.To{this.StringCaseConversion}();");

		return validation.ToString();
	}
	
	public override string GetEnumeratorCode() => $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => this.{this.PropertyName}.GetEnumerator();";

	public override string? GetExtraCode() => null;
}