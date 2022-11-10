namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// A 32-digit UUID without hyphens.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 32, maximumLength: 36, stringFormat: StringFormat.Default, stringComparison: StringComparison.Ordinal, forbidParameterlessConstruction: false, 
	addCustomValidation: false, constructorIsPublic: false, useValidationExceptions: false)]
public partial record struct Uuid
{
	public Uuid()
		: this(Guid.NewGuid().ToString("N").ToUpper())
    {
    }

    public Uuid(Guid uuid)
		: this(uuid.ToString("N").ToUpper())
    {
    }
    
    public Uuid(string value, Validator? validator = null)
		: this(value)
    {
	    validator ??= Validator.Get<Uuid>.Default;
	    validator.GuardRegex(value, "^[0-9A-F]{32}$", errorCode: null);
    }
}
