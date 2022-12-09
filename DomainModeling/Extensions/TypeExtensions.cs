namespace CodeChops.DomainDrivenDesign.DomainModeling.Extensions;

public static class TypeExtensions
{
	/// <summary>
	/// Removes the generic type information of a name that starts with a backtick.
	/// </summary>
	public static string GetSimpleName(this Type type)
	{
		var endIndex = type.Name.IndexOf('`');
		
		if (endIndex == -1) 
			return type.Name;
		
		return type.Name[..endIndex];
	}
}
