namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

/// <summary>
/// Iterates over the values of a tuple.
/// </summary>
public ref struct TupleIterator
{
	public object? Current { get; private set; }
	public int CurrentIndex { get; private set; }
	private int End { get; }
	private ImmutableList<object?> Enumerable { get; }
	
	public TupleIterator(ITuple tuple)
	{
		var enumerable = tuple.GetEnumerable().ToImmutableList();
		this.Current = 0;
		this.End = enumerable.Count - 1;
		this.Enumerable = enumerable;
	}

	public bool MoveNext()
	{
		this.CurrentIndex++;
		this.Current = this.Enumerable[this.CurrentIndex];
		return this.CurrentIndex <= this.End;
	}
}

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
	
	public static TupleIterator GetEnumerator(this ITuple tuple) 
		=> new(tuple);
}
