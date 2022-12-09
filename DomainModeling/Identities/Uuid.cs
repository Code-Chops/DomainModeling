using System.Text.RegularExpressions;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// A 32-digit UUID without hyphens.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 32, maximumLength: 36, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, useRegex: true,
	forbidParameterlessConstruction: false, useValidationExceptions: false)]
public partial record struct Uuid
{
	[GeneratedRegex("^[0-9A-F]{32}$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
	public static partial Regex ValidationRegex();
	
	public Uuid()
		: this(Guid.NewGuid().ToString("N").ToUpper())
    {
    }

    public Uuid(Guid uuid)
		: this(uuid.ToString("N").ToUpper())
    {
    }
}
