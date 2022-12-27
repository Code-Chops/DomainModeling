// ReSharper disable NotAccessedPositionalProperty.Global
namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public sealed record DefaultValueObject(
		INamedTypeSymbol ValueObjectType,
		INamedTypeSymbol UnderlyingType,
		AttributeData Attribute,
		TypeDeclarationSyntax TypeDeclarationSyntax,
		int? MinimumValue,
		int? MaximumValue,
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
		UseValidationExceptions: UseValidationExceptions,
		ValueObjectType: ValueObjectType,
		UnderlyingTypeName: GetUnderlyingTypeName(UnderlyingType, AllowNull),
		UnderlyingTypeNameBase: null,
		GenerateToString: GenerateToString,  
		GenerateComparison: GenerateComparison,
		GenerateDefaultConstructor: GenerateDefaultConstructor,
		ForbidParameterlessConstruction: ForbidParameterlessConstruction,  
		GenerateStaticDefault: GenerateStaticDefault,
		GenerateEnumerable: false,
		PropertyName: PropertyName ?? "Value",
		PropertyIsPublic: PropertyIsPublic,
		AddIComparable: true,
		AllowNull: AllowNull)
{
	private static string GetUnderlyingTypeName(ITypeSymbol type, bool valueIsNullable) 
		=> $"{type.GetTypeNameWithGenericParameters()}{(type.TypeKind is TypeKind.Struct && valueIsNullable ? "?" : null)}";

	public override string[] GetNamespaces()		=> this.UnderlyingType.ContainingNamespace.IsGlobalNamespace 
														? Array.Empty<string>() 
														: new [] { this.UnderlyingType.ContainingNamespace.ToDisplayString() };

	public override string GetCommentsCode()		=> $@"An immutable value object with an underlying value of type <see cref=""{this.ValueObjectType.GetFullTypeNameWithGenericParameters().Replace('<', '{').Replace('>', '}')}""/>.";

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
		if (!(this.UnderlyingType.IsNumeric(seeThroughNullable: true) || this.UnderlyingType.IsType<char>()) || (this.MinimumValue is null && this.MaximumValue is null)) 
			return null;

		var validationType = $"({this.UnderlyingType.Name}{(this.UnderlyingType.TypeKind is TypeKind.Struct && this.AllowNull ? "?" : null)}){this.LocalVariableName}";
		
		var underlyingTypeName = this.UnderlyingType.IsType<char>() 
			? typeof(int).FullName
			: this.UnderlyingType.Name;
		
		return this.GetGuardLine(Guard.InRange, validationType, errorCodeStart, genericParameterName: underlyingTypeName, this.MinimumValue, this.MaximumValue);
	}
	
	public override string? GetValueTransformation() => null;

	public override string? GetEnumeratorCode() => null;
	
	public override string? GetExtraCode() => null;
}
