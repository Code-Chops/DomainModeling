namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities.Extensions;

internal static class GuidExtensions
{
	public static string ConvertToString(this Guid guid)
	{
		var guidString = guid.ToString("N").ToUpper();
		return guidString;
	}
}