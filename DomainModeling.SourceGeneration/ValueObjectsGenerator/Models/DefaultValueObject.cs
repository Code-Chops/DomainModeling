namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public sealed record DefaultValueObject(
		INamedTypeSymbol Type,
		AttributeData Attribute,
		string Declaration,
		bool GenerateToString,
		bool AddCustomValidation,
		bool ProhibitParameterlessConstruction,
		bool GenerateEmptyStatic,
		int? MinimumValue,
		int? MaximumValue) 
	: ValueObjectBase(
		Type: Type,
		Declaration: Declaration,
		TypeName: Attribute.AttributeClass!.TypeArguments.Single().Name,
		ElementTypeName: Attribute.AttributeClass!.TypeArguments.Single().Name,
		GenerateToString: GenerateToString,  
		AddCustomValidation: AddCustomValidation,
		ProhibitParameterlessConstruction: ProhibitParameterlessConstruction,  
		GenerateEmptyStatic: GenerateEmptyStatic,
		GenerateComparable: true)
{
	public override string? GetNamespaces()			=> null;

	public override string GetCommentsCode()		=> $"Type {this.TypeName}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToEasyString(new {{ this.Value }});";
	
	public override string? GetInterfacesCode()		=> null;
	
	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.Value.GetHashCode();";

	public override string GetEqualsCode()			=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}? other) => {this.TypeName}.Equals(this.Value, other?.Value);";
	
	public override string GetCompareToCode()		=> $"public int CompareTo({this.Name}{this.Nullable} other) => this.Value.CompareTo(other{this.Nullable}.Value);";

	public override string GetDefaultValue()		=> $"new(default({this.TypeName}));";
	
	public override string? GetLengthOrCountCode()	=> null;

	public override string GetValidationCode()
	{
		var validation = new StringBuilder();

		if (this.MinimumValue is not null)
			validation.AppendLine($@"			if (value < {this.MinimumValue}) throw new ArgumentException($""{this.TypeName} of {this.Name} is smaller ({{value}}) than {nameof(this.MinimumValue)} {this.MinimumValue}."");");
			
		if (this.MaximumValue is not null)
			validation.AppendLine($@"			if (value > {this.MaximumValue}) throw new ArgumentException($""{this.TypeName} of {this.Name} is higher ({{value}}) than {nameof(this.MaximumValue)} {this.MaximumValue}."");");

		return validation.ToString();
	}

	public override string? GetEnumeratorCode() => null;
	
	public override string? GetExtraCode() => null;
}