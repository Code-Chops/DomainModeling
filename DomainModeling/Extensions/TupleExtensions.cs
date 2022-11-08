using System.Collections.Immutable;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Extensions;

public static class TupleExtensions
{
	public static IEnumerable<object?> GetEnumerable<TTuple>(this TTuple tuple) 
		where TTuple : ITuple
	{
		for (var i = 0; i < tuple.Length; i++)
			yield return tuple[i];
	}
	
	public static ImmutableList<object?> ToImmutableList<TTuple>(this TTuple tuple)
		where TTuple : ITuple
		=> tuple.GetEnumerable().ToImmutableList();
	
	public static object?[] ToArray<TTuple>(this TTuple tuple)
		where TTuple : ITuple
		=> tuple.GetEnumerable().ToArray();
}
