using System.Text;

namespace CodeChops.DomainModeling.Extensions;

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

	/// <summary>
	/// Gets the name of a class including the type parameters.
	/// <para>
	/// E.g: System.Collections.Generic.Dictionary`2 becomes System.Collections.Generic.Dictionary&lt;System.String,System.Object&gt;.
	/// </para>
	/// </summary>
	public static string GetNameWithTypeParameters(this Type type)
	{
		if (type.IsGenericParameter)
			return type.Name;

		if (!type.IsGenericType)
			return type.FullName ?? type.Name;

		var name = type.Name;
		var index = name.IndexOf("`", StringComparison.Ordinal);
		
		var builder = new StringBuilder();
		builder.Append($"{type.Namespace}.{name[..index]}");
		builder.Append('<');
		
		var first = true;
		foreach (var arg in type.GetGenericArguments())
		{
			if (!first)
				builder.Append(',');
			
			builder.Append(GetNameWithTypeParameters(arg));
			first = false;
		}
		builder.Append('>');
		
		return builder.ToString();
	}
}
