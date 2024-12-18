using CodeChops.SourceGeneration.Utilities;

namespace CodeChops.DomainModeling.SourceGeneration.ValueObjectsGenerator.Models;

public sealed record DefaultValueObject : ValueObjectBase
{
	public ITypeSymbol UnderlyingType { get; } = null!;
	public TypeDeclarationSyntax TypeDeclarationSyntax { get; } = null!;
	public int? MinimumValue { get; }
	public int? MaximumValue { get; }

	public DefaultValueObject(
		INamedTypeSymbol valueObjectType,
		INamedTypeSymbol? providedUnderlyingType,
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
		bool useValidationExceptions,
		bool useCustomProperty)
		: base(
			UseValidationExceptions: useValidationExceptions,
			ValueObjectType: valueObjectType,
			GenerateToString: generateToString,
			GenerateComparison: generateComparison && ImplementsIComparable(GetUnderlyingType(valueObjectType, providedUnderlyingType, out _)),
			GenerateDefaultConstructor: generateDefaultConstructor && !useCustomProperty,
			ForbidParameterlessConstruction: forbidParameterlessConstruction,
			GenerateStaticDefault: generateStaticDefault,
			GenerateEnumerable: false,
			PropertyName: propertyName ?? "Value",
			PropertyIsPublic: propertyIsPublic,
			AddIComparable: true,
			AllowNull: allowNull,
			UseCustomProperty: useCustomProperty)
	{
		var underlyingType = GetUnderlyingType(valueObjectType, providedUnderlyingType, out var parameterSubstitute);

		if (underlyingType is null)
		{
			this.ErrorMessage = "Underlying type unknown. No underlying type provided as attribute type argument, or as type parameter on the type.";
			return;
		}

		this.UnderlyingType = underlyingType;
		this.ParameterSubstitute = parameterSubstitute;
		this.UnderlyingTypeName = this.GetUnderlyingTypeName();
		this.UnderlyingTypeNameBase = null;
		this.TypeDeclarationSyntax = typeDeclarationSyntax;
		this.MinimumValue = minimumValue;
		this.MaximumValue = maximumValue;
	}

	private ITypeSymbol? ParameterSubstitute { get; }

	private static ITypeSymbol? GetUnderlyingType(INamedTypeSymbol valueObjectType, ITypeSymbol? providedUnderlyingType, out ITypeSymbol? parameterSubstitute)
	{
		var typeParameter = valueObjectType.TypeArguments.FirstOrDefault();

		if (typeParameter is ITypeParameterSymbol namedTypeParameter)
		{
			if (providedUnderlyingType is null)
			{
				parameterSubstitute = typeParameter;
				return namedTypeParameter;
			}

			if (providedUnderlyingType is INamedTypeSymbol providedNamedUnderlyingType && providedNamedUnderlyingType.IsUnboundGenericType)
			{
				parameterSubstitute = typeParameter;
				return providedNamedUnderlyingType;
			}
		}

		parameterSubstitute = null;
		return providedUnderlyingType;
	}

	private static bool ImplementsIComparable(ITypeSymbol? underlyingType)
	{
		if (underlyingType is null)
			return false;

		return underlyingType
		    .IsOrImplementsInterface(type => type.IsType(fullTypeName: typeof(IComparable).FullName), out var interf)
		       && (interf is not INamedTypeSymbol { TypeArguments.Length: 1 } || interf.HasSingleGenericTypeArgument(underlyingType));
	}

	private string GetUnderlyingTypeName()
	{
		var name = this.UnderlyingType.GetTypeNameWithGenericParameters();

		// Replace type argument of provided underlying type if needed.
		if (this.ParameterSubstitute is not null)
		{
			if (this.UnderlyingType is INamedTypeSymbol { IsGenericType: true})
			{
				var startIndex = name.IndexOf('<');

				var parameters = NameHelpers.GetGenericParameters(name)!
					.TrimStart('<').TrimEnd('>')
					.Split(',')
					.Select((p, i) => i == 0 ? this.ParameterSubstitute.Name : p);

				name = $"{name.Substring(0, startIndex)}<{String.Join(", ", parameters)}>";
			}
			else
			{
				name = this.ParameterSubstitute.Name;
			}
		}

		return $"{name}{this.UnderlyingTypeNullOperator}";
	}

	public override string UnderlyingTypeName { get; } = null!;
	public override string? UnderlyingTypeNameBase { get; }

	public override IEnumerable<string> GetUsingNamespaces()
		=> this.UnderlyingType is INamedTypeSymbol namedTypeSymbol
		? GetAllUsingNamespacesOfType(namedTypeSymbol)
		: Array.Empty<string>();

	public override string GetComments()
	{
		var attribute = this.UnderlyingType.IsDefinition
			? "typeparamref name"
			: "see cref";

		return $@"An immutable value object with an underlying value of {(this.AllowNull ? "nullable " : null)}type <{attribute}=""{this.UnderlyingType.GetTypeNameWithGenericParameters().Replace('<', '{').Replace('>', '}')}""/>.";
	}

	public override string GetToStringCode()			=> $"public override string{this.UnderlyingTypeNullOperator} ToString() => this.{this.PropertyName}{this.UnderlyingTypeNullOperator}.ToString();";

	public override string? GetInterfacesCode()			=> null;

	public override string GetHashCodeCode()			=> $"public override int GetHashCode() => this.{this.PropertyName}.GetHashCode();";

	public override string GetEqualsCode()				=> $"public {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals({this.Name}{this.NullOperator} other) => this.{this.PropertyName}.Equals(other{this.NullOperator}.{this.PropertyName});";
	public override string GetObjectEqualsCode()		=> this.ValueObjectType.IsRefLikeType
														? $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => false;"
														: $"public override {(this.IsUnsealedRecordClass ? "virtual " : null)}bool Equals(object? other) => other is {this.Name} {this.LocalVariableName} && this.Equals({this.LocalVariableName});";

	public override string GetCompareToCode()			=> $"public int CompareTo({this.Name}{this.NullOperator} other) => Comparer<{this.UnderlyingTypeName}{this.NullOperator}>.Default.Compare(this.{this.PropertyName}, other{this.NullOperator}.{this.PropertyName});";

	public override string GetDefaultValue()			=> $"default({this.UnderlyingTypeName})";

	public override string? GetLengthOrCountCode()		=> null;

	public override string? GetExtraCastCode()			=> null;

	public override string? GetExtraConstructorCode()	=> null;

	public override string? GetValidationCode(string errorCodeStart)
	{
		if (!this.UnderlyingType.IsOrImplementsInterface(type => type.IsType(typeof(IConvertible)), out _))
			return null;

		if (this.MinimumValue is null && this.MaximumValue is null)
			return null;

		var underlyingTypeName = this.UnderlyingType.IsNumeric(seeThroughNullable: true)
			? this.UnderlyingTypeName.TrimEnd('?')
			: typeof(int).FullName;

		var validationType = $"({underlyingTypeName}){this.LocalVariableName}";

		var code = new StringBuilder();

		var guardLine = this.GetGuardLine(
			Guard.InRange,
			validationType,
			errorCodeStart,
			genericParameterName: underlyingTypeName,
			this.MinimumValue is null ? $"({underlyingTypeName}?)null" : this.MinimumValue,
			this.MaximumValue is null ? $"({underlyingTypeName}?)null" : this.MaximumValue);

		if (this.AllowNull)
			code.Append($@"
		if ({this.LocalVariableName} is not null)
	");

		code.AppendLine(guardLine);

		return code.ToString();
	}

	public override string? GetValueTransformation() => null;

	public override string? GetEnumeratorCode() => null;

	public override string? GetExtraCode() => null;
}
