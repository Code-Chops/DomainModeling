using System.Text.RegularExpressions;

namespace CodeChops.DomainModeling.Identities;

/// <summary>
/// A 32-digit UUID without hyphens.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 32, maximumLength: 36, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, useRegex: true, propertyIsPublic: true,
	forbidParameterlessConstruction: false, useValidationExceptions: false)]
public partial record struct Uuid : IId<Uuid, string>
{
	[GeneratedRegex("^[0-9A-F]{32}$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
	public static partial Regex ValidationRegex();
    
	public bool HasDefaultValue => this.Value == "";
	
	public Uuid(Validator? validator = null)
		: this(Guid.NewGuid().ToString("N").ToUpper(), validator)
    {
    }

    public Uuid(Guid uuid, Validator? validator = null)
		: this(uuid.ToString("N").ToUpper(), validator)
    {
    }
}
