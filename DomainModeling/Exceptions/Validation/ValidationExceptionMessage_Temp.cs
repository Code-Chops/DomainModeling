﻿// <auto-generated />
#nullable enable
#pragma warning disable CS0612 // Is deprecated (level 1)
#pragma warning disable CS0618 // Member is obsolete (level 2)

using System;
using System.Collections;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using CodeChops.DomainDrivenDesign.DomainModeling;
using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;
using CodeChops.DomainDrivenDesign.DomainModeling.Validation;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

// Added source generated content manually here, because generic attributes still don't work in Blazor: https://github.com/dotnet/runtime/issues/77047

/// <summary>
/// An immutable value object with an underlying value of type (String, ImmutableList&lt;Object&gt;).
/// Extends: <see cref="global::CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation.ValidationExceptionMessage"/>.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly partial record struct ValidationExceptionMessage : IValueObject, ICreatable<ValidationExceptionMessage, (String, ImmutableList<Object>)>, IEquatable<ValidationExceptionMessage>, IComparable<ValidationExceptionMessage>
{
	public override partial string ToString();

	#region ValueProperty
	/// <summary>
	/// Get the underlying structural value.
	/// </summary>
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	private (String, ImmutableList<Object>) Value => this._value3720;

	/// <summary>
	/// Backing field for <see cref='Value'/>.  Don't use this field, use the Value property instead.
	/// </summary>
	[Obsolete("Don't use this field, use the Value property instead.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	private readonly (String, ImmutableList<Object>) _value3720 = default((String, ImmutableList<Object>));
	#endregion

	#region Equals
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int GetHashCode() => this.Value.GetHashCode(); 

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool Equals(ValidationExceptionMessage other) => this.Value.Equals(other.Value);
	#endregion

	#region Comparison
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int CompareTo(ValidationExceptionMessage other) => this.Value.CompareTo(other.Value);

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <	(ValidationExceptionMessage left, ValidationExceptionMessage right)	=> left.CompareTo(right) <	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <=	(ValidationExceptionMessage left, ValidationExceptionMessage right)	=> left.CompareTo(right) <= 0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >	(ValidationExceptionMessage left, ValidationExceptionMessage right)	=> left.CompareTo(right) >	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >=	(ValidationExceptionMessage left, ValidationExceptionMessage right)	=> left.CompareTo(right) >= 0;
	#endregion

	#region Casts
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static implicit operator (String, ImmutableList<Object>)(ValidationExceptionMessage value) => value.Value;

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static explicit operator ValidationExceptionMessage((String, ImmutableList<Object>) value) => new(value);
	#endregion

	#region Constructors
	[DebuggerHidden] 
	public ValidationExceptionMessage((String, ImmutableList<Object>) value, Validator? validator = null)
	{	
		validator ??= Validator.Get<ValidationExceptionMessage>.Default;

		this._value3720 = value;
	}

	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
	[Obsolete("Don't use this empty constructor. A (String, ImmutableList<Object>) should be provided when initializing ValidationExceptionMessage.", true)]
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ValidationExceptionMessage() => throw new InvalidOperationException($"Don't use this empty constructor. A (String, ImmutableList<Object>) should be provided when initializing ValidationExceptionMessage.");
	#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor
	#endregion

	#region Factories
	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate((String, ImmutableList<Object>) value, out ValidationExceptionMessage createdObject)
		=> ICreatable<ValidationExceptionMessage, (String, ImmutableList<Object>)>.TryCreate(value, out createdObject, out _);

	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate((String, ImmutableList<Object>) value, out ValidationExceptionMessage createdObject, out Validator validator)
		=> ICreatable<ValidationExceptionMessage, (String, ImmutableList<Object>)>.TryCreate(value, out createdObject, out validator);

	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static ValidationExceptionMessage Create((String, ImmutableList<Object>) value, Validator? validator = null) 
		=> new(value, validator);
	#endregion
}

#pragma warning restore CS0618 // Member is obsolete (level 2)
#pragma warning restore CS0612 // Is deprecated (level 1)
#nullable restore
