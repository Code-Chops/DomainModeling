// ReSharper disable NotAccessedPositionalProperty.Global
namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public sealed record DefaultValueObject(
		bool UseValidationExceptions,
		int? MinimumValue,
		int? MaximumValue,
		INamedTypeSymbol ValueObjectType,
		AttributeData Attribute,
		TypeDeclarationSyntax TypeDeclarationSyntax,
		bool GenerateToString,
		bool GenerateComparison,
		bool AddCustomValidation,
		bool ConstructorIsPublic,
		bool ForbidParameterlessConstruction,
		bool GenerateStaticDefault,
		string? PropertyName,
		bool PropertyIsPublic,
		bool AllowNull) 
	: ValueObjectBase(		
		UseValidationExceptions: UseValidationExceptions,
		ValueObjectType: ValueObjectType,
		UnderlyingTypeName: GetUnderlyingTypeName(GetUnderlyingType(Attribute), AllowNull),
		UnderlyingTypeNameBase: null,
		GenerateToString: GenerateToString,  
		GenerateComparison: GenerateComparison,
		AddCustomValidation: AddCustomValidation,
		ConstructorIsPublic: ConstructorIsPublic,
		ForbidParameterlessConstruction: ForbidParameterlessConstruction,  
		GenerateStaticDefault: GenerateStaticDefault,
		GenerateEnumerable: false,
		PropertyName: PropertyName ?? "Value",
		PropertyIsPublic: PropertyIsPublic,
		AddIComparable: true,
		AllowNull: AllowNull)
{
	private static ITypeSymbol GetUnderlyingType(AttributeData attribute) 
		=> attribute.AttributeClass!.TypeArguments.Single();
	private static string GetUnderlyingTypeName(ITypeSymbol type, bool allowNull) 
		=> $"{type.GetTypeNameWithGenericParameters()}{(type.TypeKind is TypeKind.Struct && allowNull ? "?" : null)}";

	public override string[] GetNamespaces()		=> Array.Empty<string>();

	public override string GetCommentsCode()		=> $"An immutable value object with an underlying value of type {this.UnderlyingTypeName.Replace("<", "&lt;").Replace(">", "&gt;")}.";

	public override string GetToStringCode()		=> $"public override string ToString() => this.ToDisplayString(new {{ this.{this.PropertyName} }});";
	
	public override string? GetInterfacesCode()		=> null;
	
	public override string GetHashCodeCode()		=> $"public override int GetHashCode() => this.{this.PropertyName}.GetHashCode();";

	public override string GetEqualsCode()			=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other) => this.{this.PropertyName}.Equals(other{this.NullOperator}.{this.PropertyName});";
	public override string GetObjectEqualsCode()	=> this.ValueObjectType.IsRefLikeType
														? $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => false;" 
														: $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string GetCompareToCode()		=> $"public int CompareTo({this.Name}{this.NullOperator} other) => this.{this.PropertyName}.CompareTo(other{this.NullOperator}.{this.PropertyName});";

	public override string GetDefaultValue()		=> $"default({this.UnderlyingTypeName})";
	
	public override string? GetLengthOrCountCode()	=> null;

	public override string? GetExtraCastCode()		=> null;
	
	public override string? GetValidationCode(string errorCodeStart)
	{
		var underlyingType = GetUnderlyingType(this.Attribute);
		
		if (!underlyingType.GetAvailableConversionsFromPrimitives(skipForSystemTypes: false).Contains(typeof(int)) || (this.MinimumValue is null && this.MaximumValue is null)) 
			return null;

		var validationType = $"({underlyingType.Name}{(underlyingType.TypeKind is TypeKind.Struct && this.AllowNull ? "?" : null)}){this.LocalVariableName}";
		
		return this.GetGuardLine(Guard.InRange, validationType, errorCodeStart, genericParameterName: underlyingType.Name, this.MinimumValue, this.MaximumValue);
	}
	
	public override string? GetValueTransformation() => null;

	public override string? GetEnumeratorCode() => null;
	
	public override string? GetExtraCode() => null;
}
