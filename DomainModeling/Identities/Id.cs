using System.Runtime.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// <para>
/// An abstract identifier with a generic type as primitive value.
/// </para>
/// <para>
/// <b>Prefer to use ID generation <see cref="GenerateStronglyTypedId{TNumber}"/> in order to use struct IDs.</b>
/// </para>
/// </summary>
/// <typeparam name="TPrimitive">The primitive value of the identifier.</typeparam>
public abstract record Id<TSelf, TPrimitive> : IId<TPrimitive>
	where TSelf : IId<TPrimitive>
	where TPrimitive : IEquatable<TPrimitive>, IComparable<TPrimitive>
{
	public override string ToString() => this.ToDisplayString(new { this.Value, PrimitiveType = typeof(TPrimitive).Name });

	public TPrimitive Value { get; protected init; }

	/// <summary>
	/// Create new instances when explicitly casting. Used to avoid the new() constraint.
	/// </summary>
	private static readonly Id<TSelf, TPrimitive> CachedUninitializedMember = (Id<TSelf, TPrimitive>)FormatterServices.GetUninitializedObject(typeof(Id<TSelf, TPrimitive>));

	public static explicit operator Id<TSelf, TPrimitive>(TPrimitive value) => CachedUninitializedMember with { Value = value };
	public static implicit operator TPrimitive(Id<TSelf, TPrimitive> id) => id.Value;

	#region Comparison
	public int CompareTo(IId? other) 
		=> other is null ? 1 : this.Value.CompareTo((TPrimitive)other.GetValue());
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <	(Id<TSelf, TPrimitive> left, IId right)	=> left.CompareTo(right) <	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=	(Id<TSelf, TPrimitive> left, IId right)	=> left.CompareTo(right) <= 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >	(Id<TSelf, TPrimitive> left, IId right)	=> left.CompareTo(right) >	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=	(Id<TSelf, TPrimitive> left, IId right)	=> left.CompareTo(right) >= 0;
	#endregion

	/// <summary>
	/// Warning. Performs boxing!
	/// </summary>
	public object GetValue() => this.Value;

	public bool HasDefaultValue => this.Value.Equals(IId<TPrimitive>.DefaultValue);

	protected Id(TPrimitive value)
	{
		this.Value = value;
	}
	
	protected Id()
	{
		this.Value = default!;
	}
}