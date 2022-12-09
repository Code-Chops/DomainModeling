namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IId<in TSelf, out TPrimitive> : IId<TPrimitive>, IComparable<TSelf>
	where TSelf : IId<TSelf, TPrimitive>
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
}

public interface IId<out TPrimitive> : IId
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	protected static readonly TPrimitive DefaultValue = default!;
	
	TPrimitive Value { get; }
}

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }

	object GetValue();
}
