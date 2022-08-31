// ReSharper disable NotAccessedPositionalProperty.Global
namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public sealed record DefaultValueObject(
		INamedTypeSymbol ValueObjectType,
		AttributeData Attribute,
		string Declaration,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool GenerateDefaultConstructor,
		bool GenerateParameterlessConstructor,
		bool GenerateEmptyStatic,
		string? PropertyName,
		bool AllowNull,
		int? MinimumValue,
		int? MaximumValue) 
	: ValueObjectBase(
		ValueObjectType: ValueObjectType,
		Declaration: Declaration,
		UnderlyingTypeName: GetUnderlyingTypeName(Attribute, AllowNull),
		GenerateToString: GenerateToString,  
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		GenerateParameterlessConstructor: GenerateParameterlessConstructor,  
		GenerateEmptyStatic: GenerateEmptyStatic,
		PropertyName: PropertyName ?? "Value",
		AddIComparable: true)
{
	public static string GetUnderlyingTypeName(AttributeData attribute, bool allowNull)
	{
		var data = attribute.AttributeClass!.TypeArguments.Single();
		return $"{data.Name}{(data.TypeKind is TypeKind.Struct && allowNull ? "?" : null)}";
	}
	
	public override string[] GetNamespaces()		=> Array.Empty<string>();

	public override string GetCommentsCode()		=> $"Type {this.UnderlyingTypeName}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToEasyString(new {{ this.{this.PropertyName} }});";
	
	public override string? GetInterfacesCode()		=> null;
	
	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.{this.PropertyName}.GetHashCode();";

	public override string GetEqualsCode()			=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other) => {this.UnderlyingTypeName}.Equals(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName});";
	public override string GetObjectEqualsCode()	=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string GetCompareToCode()		=> $"public int CompareTo({this.Name}{this.NullOperator} other) => this.{this.PropertyName}.CompareTo(other{this.NullOperator}.{this.PropertyName});";

	public override string GetDefaultValue()		=> $"default({this.UnderlyingTypeName})";
	
	public override string? GetLengthOrCountCode()	=> null;

	public override string GetValidationCode()
	{
		var validation = new StringBuilder();

		if (this.MinimumValue is not null)
			validation.AppendLine($@"			if (value < {this.MinimumValue}) throw new ArgumentException($""{{this}} is smaller than {nameof(this.MinimumValue)} {this.MinimumValue}."");");
			
		if (this.MaximumValue is not null)
			validation.AppendLine($@"			if (value > {this.MaximumValue}) throw new ArgumentException($""{{this}} is higher than {nameof(this.MaximumValue)} {this.MaximumValue}."");");

		return validation.ToString();
	}

	public override string? GetEnumeratorCode() => null;
	
	public override string? GetExtraCode() => null;
}