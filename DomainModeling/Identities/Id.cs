using System.Runtime.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// An abstract identifier with a generic type as primitive value.
/// </summary>
/// <typeparam name="TPrimitive">The primitive value of the identifier.</typeparam>
public abstract record Id<TSelf, TPrimitive> : Id<TPrimitive>, IComparable<TSelf>
	where TSelf : Id<TSelf, TPrimitive>
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	/// <summary>
	/// Create new instances when explicitly casting. Used to avoid the new() constraint.
	/// </summary>
	private static readonly TSelf CachedUninitializedMember = (TSelf)FormatterServices.GetUninitializedObject(typeof(TSelf));

	public int CompareTo(TSelf? other) => other is null ? 1 : this.Value.CompareTo(other.Value);

	public static explicit operator Id<TSelf, TPrimitive>(TPrimitive value) => CachedUninitializedMember with { _value = value };
	public static implicit operator TPrimitive(Id<TSelf, TPrimitive> id) => id.Value;
	
	protected Id(TPrimitive value)
		: base(value)
	{
	}

	protected Id()
	{
	}
}

public abstract record Id<TPrimitive> : Id, IId<TPrimitive>
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	public override string ToString() => this.ToEasyString(new { this.Value, PrimitiveType = typeof(TPrimitive).Name });
	
	/// <summary>
	/// Warning. Probably performs boxing!
	/// </summary>
	public sealed override object GetValue() => this.Value;

	public TPrimitive Value => this._value;
	// ReSharper disable once InconsistentNaming
	protected TPrimitive _value { get; init; }
	
	public sealed override bool HasDefaultValue => this.Value.Equals(DefaultValue);
	private static readonly TPrimitive DefaultValue = default!;

	protected Id(TPrimitive value)
	{
		this._value = value;
	}
	
	protected Id()
	{
		this._value = default!;
	}
}

public abstract record Id : IId
{
	public override string ToString() => this.ToEasyString(new { Value = this.GetValue() });
	public abstract object GetValue();
	public abstract bool HasDefaultValue { get; }
}