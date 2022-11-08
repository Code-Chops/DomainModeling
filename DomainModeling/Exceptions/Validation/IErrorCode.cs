namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// <para>This code can be communicated externally and can be used by clients to provided resource translations to the end-user.</para>
/// </summary>
public interface IErrorCode
{
	string Value { get; }
}
