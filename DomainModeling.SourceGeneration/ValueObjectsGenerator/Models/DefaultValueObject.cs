// ReSharper disable NotAccessedPositionalProperty.Global

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public sealed record DefaultValueObject : ValueObjectBase
{
	public INamedTypeSymbol UnderlyingType { get; }
	public AttributeData Attribute { get; }
	public TypeDeclarationSyntax TypeDeclarationSyntax { get; }
	public int? MinimumValue { get; }
	public int? MaximumValue { get; }
	
	public DefaultValueObject(
		INamedTypeSymbol valueObjectType,
		INamedTypeSymbol underlyingType,
		AttributeData attribute,
		TypeDeclarationSyntax typeDeclarationSyntax,
		int? minimumValue,
		int? maximumValue,
		bool generateToString,
		bool generateComparison,
		bool generateDefaultConstructor,
		bool forbidParameterlessConstruction,
		bool generateStaticDefault,
		string? propertyName,
		bool propertyIsPublic,
		bool allowNull, 
		bool useValidationExceptions) 
		: base(UseValidationExceptions: useValidationExceptions,
		ValueObjectType: valueObjectType,
		UnderlyingTypeName: GetUnderlyingTypeName(underlyingType, allowNull),
		UnderlyingTypeNameBase: null,
		GenerateToString: generateToString,  
		GenerateComparison: generateComparison && ImplementsIComparable(underlyingType),
		GenerateDefaultConstructor: generateDefaultConstructor,
		ForbidParameterlessConstruction: forbidParameterlessConstruction,  
		GenerateStaticDefault: generateStaticDefault,
		GenerateEnumerable: false,
		PropertyName: propertyName ?? "Value",
		PropertyIsPublic: propertyIsPublic,
		AddIComparable: true,
		AllowNull: allowNull)
	{
		this.UnderlyingType = underlyingType;
		this.Attribute = attribute;
		this.TypeDeclarationSyntax = typeDeclarationSyntax;
		this.MinimumValue = minimumValue;
		this.MaximumValue = maximumValue;
	}

	private static bool ImplementsIComparable(ITypeSymbol underlyingType) 
		=> underlyingType.IsOrImplementsInterface(type => type.IsType(fullTypeName: typeof(IComparable).FullName), out var interf) 
	        && (interf is not INamedTypeSymbol { TypeArguments.Length: 1 } || interf.HasSingleGenericTypeArgument(underlyingType));

	private static string GetUnderlyingTypeName(ITypeSymbol underlyingType, bool valueIsNullable) 
		=> $"{underlyingType.GetTypeNameWithGenericParameters()}{(underlyingType.TypeKind is TypeKind.Struct && valueIsNullable ? "?" : null)}";

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
		if (!this.UnderlyingType.IsOrImplementsInterface(type => type.IsType(typeof(IConvertible)), out _))
			return null;
		
		if (this.MinimumValue is null && this.MaximumValue is null)
			return null;

		var validationType = $"({this.UnderlyingTypeName}){this.LocalVariableName}";
		
		var underlyingTypeName = this.UnderlyingType.IsNumeric(seeThroughNullable: true)
			? this.UnderlyingTypeName.TrimEnd('?')
			: typeof(int).FullName;
		
		return this.GetGuardLine(Guard.InRange, validationType, errorCodeStart, genericParameterName: underlyingTypeName, this.MinimumValue, this.MaximumValue);
	}
	
	public override string? GetValueTransformation() => null;

	public override string? GetEnumeratorCode() => null;
	
	public override string? GetExtraCode() => null;
}
