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
		INamedTypeSymbol Type,
		AttributeData Attribute,
		string Declaration,
		bool GenerateToString,
		bool AddCustomValidation,
		bool ProhibitParameterlessConstruction,
		bool GenerateEmptyStatic,
		int? MinimumLength,
		int? MaximumLength,
		StringCaseConversion StringCaseConversion,
		StringFormat StringFormat,
		StringComparison CompareOptions) 
	: ValueObjectBase(
		Type: Type,
		Declaration: Declaration,
		TypeName: nameof(String),
		ElementTypeName: nameof(Char), 
		GenerateToString: GenerateToString,
		AddCustomValidation: AddCustomValidation,
		ProhibitParameterlessConstruction: ProhibitParameterlessConstruction,
		GenerateEmptyStatic: GenerateEmptyStatic,
		GenerateComparable: true)
{
	public override string? GetNamespaces()			=> null;
	
	public override string GetCommentsCode()		=> $"A {(this.StringCaseConversion == StringCaseConversion.NoConversion ? null : $"{this.StringCaseConversion} ")}{this.StringFormat} string.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToEasyString(new {{ this.Value }});";
	
	public override string GetInterfacesCode()		=> $"IEnumerable<{this.ElementTypeName}>";

	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.Value.GetHashCode();";

	public override string GetEqualsCode()			=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}? other) => String.Equals(this.Value, other?.Value, StringComparison.{this.CompareOptions});";

	public override string GetCompareToCode()		=> $"public int CompareTo({this.Name}{this.Nullable} other) => String.Compare(this.Value, other{this.Nullable}.Value, StringComparison.{this.CompareOptions});";

	public override string GetDefaultValue()		=> $"new(\"\");";
	
	public override string GetLengthOrCountCode()	=> $"public int Length => this.Value.Length;";

	public override string GetValidationCode()
	{
		var validation = new StringBuilder();

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

			validation.AppendLine($@"			if (Regex.IsMatch(value, ""{formatRegex}"", RegexOptions.Compiled)) throw new ArgumentException(""Invalid characters in {this.Name} of format {this.StringFormat}. Value '{{value}}'."");");
		}
			
		if (this.MinimumLength is not null)
			validation.AppendLine($@"			if (value.Length < {this.MinimumLength}) throw new ArgumentException($""String of {this.Name} is shorter ({{value.Length}}) than {nameof(this.MinimumLength)} {this.MinimumLength}."");");
			
		if (this.MaximumLength is not null)
			validation.AppendLine($@"			if (value.Length > {this.MaximumLength}) throw new ArgumentException($""String of {this.Name} is longer ({{value.Length}}) than {nameof(this.MaximumLength)} {this.MaximumLength}."");");

		if (this.StringCaseConversion is not StringCaseConversion.NoConversion)
			validation.AppendLine($"			value = value.To{this.StringCaseConversion}();");

		return validation.ToString();
	}
	
	public override string GetEnumeratorCode() => $"public IEnumerator<{this.ElementTypeName}> GetEnumerator() => this.Value.GetEnumerator();";

	public override string? GetExtraCode() => null;
}