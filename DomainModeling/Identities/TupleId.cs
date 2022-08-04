using System.Runtime.CompilerServices;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public abstract record TupleId<TSelf, TTuple> : Id<TSelf, TTuple>
	where TSelf : TupleId<TSelf, TTuple>, new()
	where TTuple : IStructuralEquatable, IStructuralComparable, IComparable, ITuple, IEquatable<TTuple>, IComparable<TTuple>;