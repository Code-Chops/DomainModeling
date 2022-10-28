using System.Text.RegularExpressions;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// A 32-digit UUID without hyphens.
/// </summary>
[GenerateStringValueObject(addParameterlessConstructor: true)]
public partial record struct Uuid
{
    private static readonly Regex ValidationRegex = new("^[0-9A-F]{32}$", RegexOptions.Compiled);

    public void Validate()
    {
        if (!ValidationRegex.IsMatch(this.Value))
            throw new ArgumentException($"Invalid GUID '{this.Value}' provided.");
    }

    public Uuid()
    {
        this.Value = Guid.NewGuid().ToString("N").ToUpper();
    }

    public Uuid(Guid uuid)
    {
        this.Value = uuid.ToString("N").ToUpper();
    }
}
