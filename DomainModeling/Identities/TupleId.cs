namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// An ID consisting of other IDs. Be aware that the individual types of the tuple elements implement IEquatable and IComparable.
/// </summary>
public abstract record TupleId<TSelf, TTuple> : Id<TSelf, TTuple>
	where TSelf : TupleId<TSelf, TTuple>
	where TTuple : IStructuralEquatable, IStructuralComparable, IComparable, ITuple, IEquatable<TTuple>, IComparable<TTuple>, new()
{
	protected TupleId(TTuple value)
		: base(value)
	{
	}

	protected TupleId()
		: base(new TTuple())
	{
	}
}