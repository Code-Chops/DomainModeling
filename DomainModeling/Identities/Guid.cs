using System.Text.RegularExpressions;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Extensions;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// A 32-digit UUID without hyphens.
/// </summary>
public abstract record Guid<TSelf> : Id<TSelf, string>
    where TSelf : Guid<TSelf>
{
    private static readonly Regex ValidationRegex = new("^[0-9A-F]{32}$", RegexOptions.Compiled);

    protected Guid(string guid)
        : base(ValidationRegex.IsMatch(guid) 
            ? guid 
            : throw new ArgumentException($"Invalid GUID provided for {typeof(Guid<TSelf>).Name}.", nameof(guid)))
    {
    }

    protected Guid()
        : base(Guid.NewGuid().ConvertToString())
    {
    }

    protected Guid(Guid guid)
        : base(guid.ConvertToString())
    {
    }
}