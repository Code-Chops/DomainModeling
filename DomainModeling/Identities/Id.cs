using System.Runtime.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
///  An abstract identifier with an ulong as primitive value (default).
/// </summary>
public abstract record Id<TSelf>(ulong Value) : Id<TSelf, ulong>(Value)
	where TSelf : Id<TSelf, ulong>;

/// <summary>
/// An abstract identifier with a generic type as primitive value.
/// </summary>
/// <typeparam name="TValue">The primitive value of the identifier.</typeparam>
/// <typeparam name="TSelf"></typeparam>
public abstract record Id<TSelf, TValue> : Id, IId<TValue>
	where TSelf : Id<TSelf, TValue>
	where TValue : IEquatable<TValue>, IComparable<TValue>
{
	public override string ToString() => $"{{{this.GetType().Name} Id={this.Value}}}";

	/// <summary>
	/// Used to create new instances when explicitly casting.
	/// </summary>
	private static readonly TSelf CachedUninitializedMember = (TSelf)FormatterServices.GetUninitializedObject(typeof(TSelf));

	public static explicit operator Id<TSelf, TValue>(TValue value) => CachedUninitializedMember with { _value = value };
	public static implicit operator TValue(Id<TSelf, TValue> id) => id.Value;

	/// <summary>
	/// Warning. Probably performs boxing!
	/// </summary>
	public override object GetValue() => this.Value;

	// ReSharper disable once MemberCanBePrivate.Global
	public TValue Value => this._value;
	// ReSharper disable once MemberCanBePrivate.Global
	protected TValue _value { get; init; }
	
	public override bool HasDefaultValue => this.Value.Equals(DefaultValue);
	private static readonly TValue DefaultValue = default!;

	protected Id(TValue? value = default)
	{
		this._value = value ?? throw new InvalidOperationException($"Can't create ID {this.GetType().Name} with a NULL-value ({typeof(TValue).Name}).");
	}
}

public abstract record Id : ValueObject, IId
{
	public abstract object GetValue();
	public abstract bool HasDefaultValue { get; }
}