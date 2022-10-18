namespace CodeChops.DomainDrivenDesign.DomainModeling.Collections;

public static class Extensions
{
	public static CustomIterator GetEnumerator(this Range range)
	{
		return new CustomIterator(range);
	}
}

public ref struct CustomIterator
{
	// ReSharper disable once MemberCanBePrivate.Global
	public double Current { get; private set; }
	private int End { get; }
	
	public CustomIterator(Range range)
	{
		if (range.End.IsFromEnd)
			throw new InvalidOperationException($"Cannot iterate a range from start {range.Start} until {range.End}.");
		
		this.Current = range.Start.Value - 1;
		this.End = range.End.Value;
	}

	// ReSharper disable once UnusedMember.Global
	public bool MoveNext()
	{
		this.Current++;
		return this.Current <= this.End;
	}
}