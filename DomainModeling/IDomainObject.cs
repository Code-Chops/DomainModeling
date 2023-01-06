namespace CodeChops.DomainModeling;

/// <summary>
/// An object in the domain model.
/// </summary>
public interface IDomainObject
{
	/// <summary>
	/// Use <see cref="DisplayStringExtensions.ToDisplayString{TDomainObject}"/> for easy string display.
	/// </summary>
	string? ToString();
}
