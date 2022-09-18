using System.Runtime.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// An abstract identifier with a generic type as primitive value.
/// </summary>
/// <typeparam name="TPrimitive">The primitive value of the identifier.</typeparam>
public abstract record Id<TSelf, TPrimitive> : Id<TPrimitive>
	where TSelf : Id<TSelf, TPrimitive>
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	/// <summary>
	/// Create new instances when explicitly casting. Used to avoid the new() constraint.
	/// </summary>
	private static readonly TSelf CachedUninitializedMember = (TSelf)FormatterServices.GetUninitializedObject(typeof(TSelf));

	public static explicit operator Id<TSelf, TPrimitive>(TPrimitive value) => CachedUninitializedMember with { Value = value };
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
	
	#region Comparison
	public int CompareTo(Id<TPrimitive>? other) 
		=> other is null ? 1 : this.Value.CompareTo(other.Value);

	public override int GetHashCode() 
		=> HashCode.Combine(base.GetHashCode(), this.Value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <	(Id<TPrimitive> left, Id<TPrimitive> right)	=> left.CompareTo(right) <	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=	(Id<TPrimitive> left, Id<TPrimitive> right)	=> left.CompareTo(right) <= 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >	(Id<TPrimitive> left, Id<TPrimitive> right)	=> left.CompareTo(right) >	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=	(Id<TPrimitive> left, Id<TPrimitive> right)	=> left.CompareTo(right) >= 0;
	#endregion
	
	/// <summary>
	/// Warning. Performs boxing!
	/// </summary>
	public sealed override object GetValue() => this.Value;

	public TPrimitive Value { get; protected init; }

	public sealed override bool HasDefaultValue => this.Value?.Equals(DefaultValue) ?? true;
	private static readonly TPrimitive DefaultValue = default!;

	protected Id(TPrimitive value)
	{
		this.Value = value;
	}
	
	protected Id()
	{
		this.Value = default!;
	}
}

/// <summary>
/// Created in order to support covariant return types (because it does not have a generic type parameter).
/// </summary>
public abstract record Id : IId
{
	public override string ToString() => this.ToEasyString(new { Value = this.GetValue() });
	public abstract object GetValue();
	public abstract bool HasDefaultValue { get; }
}