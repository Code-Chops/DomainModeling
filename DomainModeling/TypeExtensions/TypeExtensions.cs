namespace CodeChops.DomainDrivenDesign.DomainModeling.TypeExtensions;

public static class TypeExtensions
{
	public static string GetCleanTypeName(this Type type)
	{
		var endIndex = type.Name.IndexOf('`');
		if (endIndex == -1) return type.Name;
		
		return type.Name[..endIndex];
	}
}