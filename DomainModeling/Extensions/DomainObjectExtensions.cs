namespace CodeChops.DomainDrivenDesign.DomainModeling.Extensions;

public static class DomainObjectExtensions
{
	public static string ToEasyString<TDomainObject>(this TDomainObject domainObject, object? parameters = null, string? extraText = null)
		where TDomainObject : IDomainObject
		=> EasyStringHelper.ToDisplayString<TDomainObject>(parameters, extraText); 
}