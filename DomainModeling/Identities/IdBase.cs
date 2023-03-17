using System.Runtime.Serialization;

namespace CodeChops.DomainModeling.Identities;

/// <summary>
/// <para>
/// An abstract identifier with a generic type as underlying value.
/// </para>
/// <para>
/// <b>Don't add extra properties (besides <see cref="Value"/>) when implementing this base class as they will be ignored.</b>
/// </para>
/// <para>
/// <em>Prefer to use ID generation <see cref="GenerateIdentity{TNumber}"/> in order to use struct IDs.</em>
/// </para>
/// </summary>
/// <typeparam name="TUnderlying">The underlying value of the identifier.</typeparam>
public abstract record Id<TSelf, TUnderlying> : IId<TSelf, TUnderlying>
	where TSelf : Id<TSelf, TUnderlying>, new() 
	where TUnderlying : IEquatable<TUnderlying?>, IComparable<TUnderlying?>
{
	public override string ToString() => this.Value.ToString()!;

	// ReSharper disable once MemberCanBePrivate.Global
	public TUnderlying Value { get; protected init; }

	public static explicit operator Id<TSelf, TUnderlying>(TUnderlying value) => new TSelf() { Value = value };
	public static implicit operator TUnderlying(Id<TSelf, TUnderlying> id) => id.Value;

	#region Comparison
	public int CompareTo(TSelf? other) 
		=> other is null ? 1 : this.Value.CompareTo(other.Value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) <	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) <= 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) >	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) >= 0;
	#endregion

	bool IId.HasDefaultValue => this.Value.Equals(default);

	protected Id(TUnderlying value)
	{
		this.Value = value;
	}
	
	protected Id()
	{
		this.Value = default!;
	}

	/// <summary>
	/// Create new instances when explicitly casting. Used to avoid the new() constraint.
	/// </summary>
	private static readonly TSelf CachedUninitializedMember = (TSelf)FormatterServices.GetUninitializedObject(typeof(TSelf));
}
