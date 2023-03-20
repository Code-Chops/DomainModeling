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
public abstract record Id<TSelf, TUnderlying> : IId<TSelf, TUnderlying>, IHasDefault<TSelf> 
	where TSelf : Id<TSelf, TUnderlying>
	where TUnderlying : IEquatable<TUnderlying?>, IComparable<TUnderlying?>
{
	public override string? ToString() => this.Value?.ToString()!;

	public TUnderlying? Value { get; init; }

	public static explicit operator Id<TSelf, TUnderlying>(TUnderlying value) => Default with { Value = value };
	public static implicit operator TUnderlying?(Id<TSelf, TUnderlying> id) => id.Value;

	#region Comparison
	public int CompareTo(TSelf? other) 
		=> other is null ? 1 : this.Value?.CompareTo(other.Value) ?? -1;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) <	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator <=	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) <= 0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) >	0;
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator >=	(Id<TSelf, TUnderlying> left, TSelf? right)	=> left.CompareTo(right) >= 0;
	#endregion

	public static TSelf Default { get; } = (TSelf)FormatterServices.GetUninitializedObject(typeof(TSelf));
	bool IId.HasDefaultValue => this.Value?.Equals(default) ?? true;
}
