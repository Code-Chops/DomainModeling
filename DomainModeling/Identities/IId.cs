namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IId<out TPrimitive> : IId, IComparable<IId>
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