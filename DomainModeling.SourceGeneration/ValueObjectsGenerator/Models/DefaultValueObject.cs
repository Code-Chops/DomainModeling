// ReSharper disable NotAccessedPositionalProperty.Global
namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public sealed record DefaultValueObject(
		INamedTypeSymbol ValueObjectType,
		AttributeData Attribute,
		TypeDeclarationSyntax TypeDeclarationSyntax,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool GenerateDefaultConstructor,
		bool AddParameterlessConstructor,
		bool GenerateStaticDefault,
		string? PropertyName,
		bool PropertyIsPublic,
		bool AllowNull,
		int? MinimumValue,
		int? MaximumValue) 
	: ValueObjectBase(
		ValueObjectType: ValueObjectType,
		TypeDeclarationSyntax: TypeDeclarationSyntax,
		UnderlyingTypeName: GetUnderlyingTypeName(Attribute, AllowNull),
		UnderlyingTypeNameBase: null,
		GenerateToString: GenerateToString,  
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		AddParameterlessConstructor: AddParameterlessConstructor,  
		GenerateStaticDefault: GenerateStaticDefault,
		GenerateEnumerable: false,
		PropertyName: PropertyName ?? "Value",
		PropertyIsPublic: PropertyIsPublic,
		AddIComparable: true)
{
	public static string GetUnderlyingTypeName(AttributeData attribute, bool allowNull)
	{
		var data = attribute.AttributeClass!.TypeArguments.Single();
		return $"{data.Name}{(data.TypeKind is TypeKind.Struct && allowNull ? "?" : null)}";
	}
	
	public override string[] GetNamespaces()		=> Array.Empty<string>();

	public override string GetCommentsCode()		=> $"Type {this.UnderlyingTypeName}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ this.{this.PropertyName} }});";
	
	public override string? GetInterfacesCode()		=> null;
	
	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.{this.PropertyName}.GetHashCode();";

	public override string GetEqualsCode()			=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other) => {this.UnderlyingTypeName}.Equals(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName});";
	public override string GetObjectEqualsCode()	=> $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string GetCompareToCode()		=> $"public int CompareTo({this.Name}{this.NullOperator} other) => this.{this.PropertyName}.CompareTo(other{this.NullOperator}.{this.PropertyName});";

	public override string GetDefaultValue()		=> $"default({this.UnderlyingTypeName})";
	
	public override string? GetLengthOrCountCode()	=> null;

	public override string? GetExtraCastCode()		=> null;
	
	public override string GetValidationCode()
	{
		var validation = new StringBuilder();

		if (this.MinimumValue is not null)
			validation.AppendLine($@"			if (value < {this.MinimumValue}) throw new ArgumentException($""{this.Name} is smaller than {nameof(this.MinimumValue)} {this.MinimumValue}."");");
			
		if (this.MaximumValue is not null)
			validation.AppendLine($@"			if (value > {this.MaximumValue}) throw new ArgumentException($""{this.Name} is higher than {nameof(this.MaximumValue)} {this.MaximumValue}."");");

		return validation.ToString();
	}

	public override string? GetEnumeratorCode() => null;
	
	public override string? GetExtraCode() => null;
}