namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

/// <summary>
/// Iterates over an index which resides in the provided range.
/// </summary>
public ref struct RangeIterator
{
	public double Current { get; private set; }
	private int End { get; }
	
	public RangeIterator(Range range)
	{
		if (range.End.IsFromEnd)
			throw new InvalidOperationException($"Cannot iterate a range from start {range.Start} until {range.End}.");
		
		this.Current = range.Start.Value - 1;
		this.End = range.End.Value;
	}

	public bool MoveNext()
	{
		this.Current++;
		return this.Current <= this.End;
	}
}

public static class RangeExtensions
{
	public static RangeIterator GetEnumerator(this Range range) 
		=> new(range);
}
