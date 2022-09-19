namespace CodeChops.DomainDrivenDesign.DomainModeling.Extensions;

public static class DomainObjectExtensions
{
	public static string ToEasyString<TDomainObject>(this TDomainObject _, object? parameters = null, string? extraText = null)
		where TDomainObject : IDomainObject
		=> DisplayStringHelper.ToDisplayString<TDomainObject>(parameters, extraText); 
}