# Domain modeling (DDD)
This packages helps with modeling your code in accordance to the principles of Domain-Driven Design (DDD). It contains base types, factories, source generation, identities, validation, and helpers to provide you a clear way to implement DDD easily.

## Base types
The base types include:

### Value objects
A value object basically wraps an underlying value and exposes an API that is relevant for the value object. There are multiple characteristics of value objects that have to be taken into account when creating them: Value objects should be immutable, lightweight, have structural equality, and therefore shouldn't contain an ID. Value objects can be implemented correctly by using one of the following methods (ordered by preference):
1. Use the **value object generator** by placing an attribute on a (record) struct/class. The parameters on the attribute will guide you to a correct implementation of the value object. The generator supports the following underlying types: `struct`, `string`, `list`, and `dictionary`. See [Value object generator](#Value-object-generator) for more information.
2. Implement the abstract `record` `ValueObject<TSelf>`. `GetHashCode` and `Equals` do not have to be implemented.
3. Implement the abstract `class`  `ValueObjectClass<TSelf>`. Not recommended: `GetHashCode` and `Equals` now have to be implemented manually. Also, it is a good convention to use records for value objects, as records enforce structural equality out-of-the-box.
	
### Entities
The abstract `Entity` enforces a correct way to implement a reference type with an identity (ID). See also [Identities](#Identities). 
Sometimes an entity contains a collection as underlying value. Therefore some commonly used collections are included in this package. 
The following base-entities can be used if an entity uses a collection as underlying value:
- Dictionary entity
- HashSet entity
- List entity

These entities publish a readonly API (indices, ContainsKey, GetEnumerator, Count, ...). This way, less boilerplate has to be written manually.

### Other types
- IDomainObject
- AggregateRoot
- IApplicationService
- IBoundedContext
- IDomainService

## Validation
Validation should be performed in the constructor of an object. A `Validator` class can be passed to constructors or factory methods [Factories](#Factories) which gives control (from the outside) over the behaviour during (in)validation.

### Validator
Provides a way to easily (in)validate domain objects. It support several modes to handle `ValidationExceptions`: 
- `Default`: throws an exception when the guard invalidates (immutable).
- `DoNotThrow`: does not throw when the guard invalidates. This is being used by the `TryCreate` factory, see [Factories](#Factories). It remembers every validation exception that occured during invalidation (mutable).
- `Oblivious`: does *not* throw. It does *not* collect validation exception (immutable).

### Guards
- Are an extension method on the `Validator` and contains logic to validate a domain object. It therefore *guards* that an object will be in a valid state. 
- Ensure that checks are centralized, can easily be re-used, and will throw consistent exception messages. Consistency of messages is especially relevant when using `Validation exceptions`, see [Validation exceptions](#ValidationExceptions).
- Help avoid the usage of the `throw` keyword in hot paths as throw keywords prevent JIT-inlining.
- Accept an `ErrorCode`. This code is a unique code (in the scope of the domain) and can be used by other services to know which error has occured. When a code has been passed to the guard, a validation exception will be thrown. If `null` is provided, a system exception will be thrown.

### Exceptions
#### ValidationExceptions 
- Are represented in code as `ValidationException<TGuard>`.
- Should occur after invalidation of external input.
- Contain an error code, a message, and values of the validation parameters, which can be communicated externally.
- This helps localization of messages shown to the end-user for external services. To consume and localize these messages, see the [DDD Contracts library](https://github.com/Code-Chops/DomainDrivenDesign.Contracts).

#### System exceptions
- Are represented in code as `SystemException<TGuard>`. 
- Contain system-information and therefore should not be visible to the user.
		
### Factories
Factories can be created by implementing the static abstract method `ICreatable<TObject,T1,T2,..)` (the value object generator will automatically create them for you). When they are implemented, a `TryCreate` or `Create` factory method can be used. The `TryCreate` method offers a good way to try to construct domain objects without having to use an expensive `try-catch` to catch the validation exception. This factory uses the `DoNotThrow` Validator-method behind the scenes.
	
## Examples

Create a value object that needs to be validated:
```cs
public enum ErrorCode
{
	Name_IsNull,
	Name_LengthOutOfRange,
	Name_Invalid,
}

public record Name : ValueObject<Name>, ICreatable<Name, string>
{
	public static Name Create(string name, Validator? validator = null)
		=> new(name, validator);

	private Name(string name, Validator? validator = null)
	{
		validator ??= Validator.Get<Name>.Default;
		
		validator.GuardNotNull(name, errorCode: ErrorCode.Name_IsNull.ToString());
		validator.GuardLengthInRange(name, 1, 15, errorCode: ErrorCode.Name_LengthOutOfRange.ToString());
		validator.GuardRegex(name, "^[a-zA-Z][a-zA-Z0-9]*$", ErrorCode.Name_Invalid.ToString());
	}
}
````

Behaviour:
- Name `null` will throw a `ValidationException<NotNullGuard<Name>>` with message *"Required data 'value' for 'Name' is missing."*.
- Name 'ThisNameIsTooLong' will throw a `ValidationException<LengthInRangeGuard>` with message *"Length is out of range for 'Name' 'ThisNameIsTooLong' (Lower bound: '1') (Upper bound: '15').'*.
- Name '1337' will throw a `ValidationException<RegexGuard>` with message *"'Name' '1337' is incorrect."*.

Now it's easy to perform `TryCreate` on the object:
```cs
public record Person(Name Name) : ValueObject<Person>
{
	public bool Exists(string nameString)
	{
		if (!ICreatable<Name, string>.TryCreate(nameString, out _))
			return false;
		
		/* etc */
		return true;
	}
}
````

# Value object generator
A value object can easily be generated by placing an attribute on an `readonly (ref) partial struct`. If this is not possible, place the attribute on a `partial record/class`). The parameters on the attribute are settings which configure the creation of the value object.

The generator correctly implements a value object for you by:
- Forcing you to think about the ToString-implementation:<br/>You can implement `ToString`, or letting you choose the default `ToDisplayString`-method. This can be configured using attribute parameter `generateToString`. See also [ToDisplayString](#ToDisplayString).
- Implementing `Equals`, `GetHashCode`, `CompareTo`, and comparison operators:<br/>This can be turned off by setting attribute parameter `generateComparison` to `false`.
- Generating a default constructor which supports `Validator` and therefore `Guards`:<br/>
It even creates some standard validation, see [Underlying types](#Underlying-types). This can be turned of by setting attribute parameter `generateDefaultConstructor` to `false`.
- Forces you to think about forbidding the usage of a parameterless constructor by default:<br/>
As structs automatically contain a parameterless constructor, something that can be forgotten easily. The generator will place an `Obsolete` attribute on a generated parameterless constructor if the required setting `forbidParameterlessConstruction` is set to `true`. When the constructor is still being called, it will throw an `InvalidOperationException`. 
- Generating a static property containing a default instance of the value object:<br/>
Value objects are immutable and therefore easily can provided a default value (if needed). This functionality can be turned on by setting `generateStaticDefault` to `true`.
- Generating default casts for you:
  - From the underlying value to the value object: `explicit cast`. As it could throw exceptions.
  - From the value object to the underlying value: `implicit cast`.
- Letting you choose the property name which holds the underlying value:<br/> 
This can be done by providing a value for parameter `propertyName`. If not provided, 'Value' will be used as property name.
- Setting the accessibility of the property by default to `private`:<br/>
This can be changed by setting `propertyIsPublic` to `true`.
- Letting you think about if a `ValidationException` or a `SystemException` should be thrown when a `Guard` invalidates:<br/>
Using the parameter `useValidationExceptions`.
- Exposing indices and `IEnumerable` `IReadOnly`-methods to value objects with an enumerable as underlying value:<br/>
This can be turned off by setting parameter `generateEnumerable` to `false`.
- Adding the keyword `readonly` automatically for structs:<br/>
This will ensure immutability and also optimize the struct.
- Letting you think about if a underlying value should be `nullable`:<br/>
Nullability can be enabled by setting `valueIsNullable` to `true`. 
- Adding the `StructLayoutAttribute` with `LayoutKind.Auto`: <br/>
It gives the CLR permission to reorder the bytes corresponding to these fields. It decides exactly how to reorganize the fields for memory usage, packing, etc. By default structs in C# are implemented with `LayoutKind.Sequential`, because if types are commonly used for COM Interop, their fields must stay in the order they were defined. But because our ValueObjects are not expected to be used for COM Interop the `LayoutKind.Auto` attribute is used to optimize the struct.
- Adding basic validation for you:<br/>
See [Underlying types](#Underlying-types).
- Forcing you to think about string-equality:<br/>
Comparison behaviour should be provided for value objects that have an underlying value of `string`.
- Generating `Length` or `Count` properties:<br/>
For string or enumerable value objects.
- Automatically implementing `IValueObject`:<br/>
Not implementing the interface can lead to unexpected and undesired behaviour in your code.

> If a default constructor has to be extended or edited: copy the generated constructor, place it in your domain object, and edit it. Subsequently, parameter `generateDefaultConstructor` has to be set to `false`. 

## Underlying types
It supports the following underlying types: `struct`, `string`, `list`, and `dictionary`. It can even generate objects with multiple underlying values in the form of `ValueTuples`, see [Tuple value object example](#Tuple-value-object-example).
 
The following attributes can be used:
- `GenerateValueObjectAttribute<T>`. A value object with a `struct` as underlying value, for example `int`, `DateTime`, `decimal`. 
- `GenerateDictionaryValueObjectAttribute<TKey, TValue> `. A value object with an immutable dictionary as underlying type. It has 2 optional settings: `minimumCount` and `maximumCount`. If provided, the default constructor will guard that the `KeyValuePair`-count lies between these values.
- `GenerateListValueObjectAttribute<T>`. A value object with an immutable list as underlying type. It has 2 optional settings: `minimumCount`, `maximumCount`. If provided, the default constructor will guard that the element-count lies between these values.
- `GenerateStringValueObjectAttribute`. A value object that holds a string as underlying value. It has multiple settings:
  - `minimumLength` and `maximumLength` (required). If provided, the default constructor will guard that the length of the string lies between these values. 
  - `useRegex` (required). If true, it will force you to implement a static method `ValidationRegex` which returns a `Regex`. This regex will be used to (in)validate the object. See the example below.
  - `stringFormat` (required). The default constructor will guard that the string is of one of the following formats: 
  	- `Default`
	- `Alpha`
	- `AlphaWithUnderscore`
	- `AlphaNumeric`
	- `AlphaNumericWithUnderscore`
  - `stringComparison` (required). Configures the way strings should be compared, see [StringComparison](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison?system-stringcomparison-ordinalignorecase).
  - `stringCaseConversion` (optional). Configures if the string should be converted to `LowerInvariant` or `UpperInvariant` automatically. Default: `NoConversion`.

> Warning! Manually added properties won't be included in the `Equals`, `CompareTo` and `GetHashCode`-calculation. To use value objects with multiple underlying values a `ValueTuple` can be provided as type parameter.
> The property should be set to `private` and non-private expression bodied properties should be created that expose each value of the tuple. See the [tuple example below](#Tuple-value-object-example).
> 

## Simple value object example
To create a simple `PlayerAge` value object. Write the following code:
```cs
[GenerateValueObject<int>(
  minimumValue: 0, 
  maximumValue: 120, 
  useValidationExceptions: false)]
public partial record struct PlayerAge;
```

It will generate the following code:
```cs
// <auto-generated />
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

/// <summary>
/// An immutable value object with an underlying value of type Int32.
/// Extends: <see cref="global::PlayerAge"/>.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly partial record struct PlayerAge : IValueObject, ICreatable<PlayerAge, Int32>, IEquatable<PlayerAge>, IComparable<PlayerAge>
{
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => this.ToDisplayString(new { this.Value });

	#region ValueProperty
	/// <summary>
	/// Get the underlying structural value.
	/// </summary>
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	private Int32 Value => this._value1332;
	
	/// <summary>
	/// Backing field for <see cref='Value'/>.  Don't use this field, use the Value property instead.
	/// </summary>
	[Obsolete("Don't use this field, use the Value property instead.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	private readonly Int32 _value1332 = default(Int32);
	#endregion

	#region Equals
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int GetHashCode() => this.Value.GetHashCode(); 

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool Equals(PlayerAge other) => this.Value.Equals(other.Value);
	#endregion

	#region Comparison
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int CompareTo(PlayerAge other) => this.Value.CompareTo(other.Value);

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <	(PlayerAge left, PlayerAge right)	=> left.CompareTo(right) <	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <=	(PlayerAge left, PlayerAge right)	=> left.CompareTo(right) <= 0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >	(PlayerAge left, PlayerAge right)	=> left.CompareTo(right) >	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >=	(PlayerAge left, PlayerAge right)	=> left.CompareTo(right) >= 0;
	#endregion

	#region Casts
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static implicit operator Int32(PlayerAge value) => value.Value;

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static explicit operator PlayerAge(Int32 value) => new(value);
	#endregion

	#region Constructors
	[DebuggerHidden] 
	public PlayerAge(Int32 value, Validator? validator = null)
	{	
		validator ??= Validator.Get<PlayerAge>.Default;

		validator.GuardInRange<Int32>((Int32)value, 0, 120, errorCode: null);

		this._value1332 = value;
	}

	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
	[Obsolete("Don't use this empty constructor. A Int32 should be provided when initializing PlayerAge.", true)]
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public PlayerAge() => throw new InvalidOperationException($"Don't use this empty constructor. A Int32 should be provided when initializing PlayerAge.");
	#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor
	#endregion

	#region Factories
	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate(Int32 value, out PlayerAge createdObject)
		=> ICreatable<PlayerAge, Int32>.TryCreate(value, out createdObject, out _);

	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate(Int32 value, out PlayerAge createdObject, out Validator validator)
		=> ICreatable<PlayerAge, Int32>.TryCreate(value, out createdObject, out validator);

	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static PlayerAge Create(Int32 value, Validator? validator = null) 
		=> new(value, validator);
	#endregion
}

#pragma warning restore CS0618 // Member is obsolete (level 2)
#pragma warning restore CS0612 // Is deprecated (level 1)
#nullable restore
```

## String value object example
The `Uuid` provided in this package is created using dogfooding principles. It is implemented as follows:
```cs
/// <summary>
/// A 32-digit UUID without hyphens.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 32, 
	maximumLength: 36, 
	useRegex: true, 
	stringFormat: StringFormat.Default, 
	stringComparison: StringComparison.Ordinal,
	forbidParameterlessConstruction: false, 
	useValidationExceptions: false)]
public partial record struct Uuid
{
	[GeneratedRegex("^[0-9A-F]{32}$", RegexOptions.CultureInvariant, matchTimeoutMilliseconds: 1000)]
	public static partial Regex ValidationRegex();
	
	public Uuid()
		: this(Guid.NewGuid().ToString("N").ToUpper())
	{
	}

	public Uuid(Guid uuid)
		: this(uuid.ToString("N").ToUpper())
	{
	}
}
```

This results in the following generated code:
```cs
// <auto-generated />
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

namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// An immutable value type with a Default-Formatted string as underlying value.
/// Extends: <see cref="global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid"/>.
/// </summary>
[StructLayout(LayoutKind.Auto)]
public readonly partial record struct Uuid : IValueObject, ICreatable<Uuid, String>, IEquatable<Uuid>, IComparable<Uuid>, IEnumerable<Char>, IHasValidationRegex
{
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ToString() => this.ToDisplayString(new { this.Value });

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int Length => this.Value.Length;

	#region ValueProperty
	/// <summary>
	/// Get the underlying structural value.
	/// </summary>
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	private String Value => this._value6796 ?? String.Empty;
	
	/// <summary>
	/// Backing field for <see cref='Value'/>.  Don't use this field, use the Value property instead.
	/// </summary>
	[Obsolete("Don't use this field, use the Value property instead.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	private readonly String _value6796 = String.Empty;
	#endregion

	#region Equals
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int GetHashCode() => String.GetHashCode(this.Value, StringComparison.Ordinal); 

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool Equals(Uuid other) => String.Equals(this.Value, other.Value, StringComparison.Ordinal);
	#endregion

	#region Comparison
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int CompareTo(Uuid other) => String.Compare(this.Value, other.Value, StringComparison.Ordinal);

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <	(Uuid left, Uuid right)	=> left.CompareTo(right) <	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator <=	(Uuid left, Uuid right)	=> left.CompareTo(right) <= 0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >	(Uuid left, Uuid right)	=> left.CompareTo(right) >	0;
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator >=	(Uuid left, Uuid right)	=> left.CompareTo(right) >= 0;
	#endregion

	#region Casts
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static implicit operator String(Uuid value) => value.Value;

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static explicit operator Uuid(String value) => new(value);
	#endregion

	#region Enumerator
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public IEnumerator<Char> GetEnumerator() => this.Value.GetEnumerator();
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	#endregion

	#region Constructors
	[DebuggerHidden] 
	public Uuid(String value, Validator? validator = null)
	{	
		validator ??= Validator.Get<Uuid>.Default;

		validator.GuardNotNull(value, errorCode: null);
		validator.GuardLengthInRange(value, 32, 36, errorCode: null);
		validator.GuardRegex(value, ValidationRegex(), errorCode: null);

		this._value6796 = value;
	}
	#endregion

	#region Factories
	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate(String value, out Uuid createdObject)
		=> ICreatable<Uuid, String>.TryCreate(value, out createdObject, out _);

	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool TryCreate(String value, out Uuid createdObject, out Validator validator)
		=> ICreatable<Uuid, String>.TryCreate(value, out createdObject, out validator);

	[DebuggerHidden] 
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static Uuid Create(String value, Validator? validator = null) 
		=> new(value, validator);
	#endregion

	#region TypeSpecific
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Char? this[int index] 
		=> Validator.Get<Uuid>.Default.GuardInRange(this.Value, index, errorCode: null)!;
	#endregion
}

#pragma warning restore CS0618 // Member is obsolete (level 2)
#pragma warning restore CS0612 // Is deprecated (level 1)
#nullable restore
```

## Tuple value object example
The `ValidationExceptionMessage` used in this package is also created using dogfooding principles. It is implemented as follows:
```cs
namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// A validation message is communicated externally and contains a string message and parameters (which can be used for String.Format).
/// </summary>
[GenerateValueObject<(string, ImmutableList<object>)>(minimumValue: 0, maximumValue: Int32.MaxValue, generateToString: false, useValidationExceptions: false)]
public readonly partial record struct ValidationExceptionMessage
{
	public override partial string ToString() => String.Format(this.Message, this.Parameters.ToArray());

	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public string Message => this.Value.Item1;
	
	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public ImmutableList<object?> Parameters => this.Value.Item2!;

	public ValidationExceptionMessage(string objectName, string message, IEnumerable<object?> parameters)
		: this((message, new object?[] { objectName}.Concat(parameters).ToImmutableList())!)
	{
	}
	
	public ValidationExceptionMessage(string objectName, string message, object? parameter)
		: this((message, new[] { objectName, parameter }.ToImmutableList())!)
	{	
	}
	
	/// <summary>
	/// Used for deserialization.
	/// </summary>
	internal ValidationExceptionMessage(string message, IEnumerable<object?> parameters)
		: this((message, parameters.ToImmutableList())!)
	{	
	}
}
```

This generates the following code:
```cs
// <auto-generated />
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
	private (String, ImmutableList<Object>) Value => this._value2512;
	
	/// <summary>
	/// Backing field for <see cref='Value'/>.  Don't use this field, use the Value property instead.
	/// </summary>
	[Obsolete("Don't use this field, use the Value property instead.")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	private readonly (String, ImmutableList<Object>) _value2512 = default((String, ImmutableList<Object>));
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

		this._value2512 = value;
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
```

# Identities
Entities require a unique identity (ID) in order to be distinguished from other entities. Each entity should have it's strongly typed ID.
It's important that the scope in which the ID should unique is taken into consideration before implementing them.
The underlying value of an identity should implement `IEquatable`. It can be one of the following types:
- A primitive type (like `string`, `ulong`, `int`, `byte`, etc.).
- A (custom implemented) `ValueObject`.
- A `Guid`.
- A `Uuid` (which is included in this package, see [Simple value object example](#Simple-value-object-example)): a 32-digit UUID without hyphens.
- Any other `struct` which implements `IEquatable`.

Identities can be implemented correctly by using one of the following methods (ordered by preference):
- Use the [Identity generator](#Identity-generator). This way you can create a readonly struct (which probably lives on the stack).
- Implementing the abstract record `IdBase`. Not recommended as this creates a reference type.
- Implementing IId. Not recommended as `Equals` and `GetHashCode` should manually correctly be implemented.

A `SingletonId<TObject>` is also supported. This is convenient for implementing IDs of objects that only have one ID per type.

> To enable correct JSON serialization of identities, add the following line to your Program.cs 
> ```cs
> builder.Services.AddIdentityJsonSerialization();
> ```

# Identity generator
An identity belongs to an entity. To create an identity for an entity, simply make the class `partial` and add the attribute `GenerateStronglyTypedId<T>` (where `T` is the underlying value). 
The type parameter `<T`> can also be omitted. In this case the underlying value of the identity will be `ulong`.
A `readonly partial struct` will be generated which is nested in the entity class. The entity will also get a property which contains an instance of the identity. 
Two parameters can be provided when adding the attribute:
- `name`. If provided, this will be the name of the identity. The default name is `Identity`.
- `propertyName`. The name of the identity-property of the entity class. The default name is `Id`.

> If entity `Player` has a `GenerateStronglyTypedId`-attribute (without provided parameters):
> - `Player.Identity` references the identity type.
> - `Player.Id` references the instance of the identity.

## Entity with identity example

The following example will generate a strongly typed identity for entity `Player`:

```cs
[GenerateStronglyTypedId<Uuid]
public partial class Player
{
}
```

The following code will be generated:
```cs
// <auto-generated />
#nullable enable

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public partial class Player : IHasId
{	
	public IId Id { get; } = new Identity();

	public readonly partial record struct Identity : IId<Identity, global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid>
	{ 
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => this.ToDisplayString(new { this.Value, PrimitiveType = nameof(global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid) });

		[EditorBrowsable(EditorBrowsableState.Never)]
		public global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid Value { get; private init; }
	
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static explicit operator Identity(global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid value) => new() { Value = value };
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static implicit operator global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid(Identity id) => id.Value;
	
		#region Comparison
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int CompareTo(Identity other) 
			=> this.Value.CompareTo((Identity)other.GetValue());
		
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <	(Identity left, Identity right)	=> left.CompareTo(right) <	0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=	(Identity left, Identity right)	=> left.CompareTo(right) <= 0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >	(Identity left, Identity right)	=> left.CompareTo(right) >	0;
		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=	(Identity left, Identity right)	=> left.CompareTo(right) >= 0;
		#endregion
	
		/// <summary>
		/// Warning. Probably performs boxing!
		/// </summary>
		[DebuggerHidden]
		public object GetValue() => this.Value;
	
		[DebuggerHidden]
		public bool HasDefaultValue => this.Value.Equals(IId<global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid>.DefaultValue);
	
		[DebuggerHidden]
		public Identity(global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid value)
		{
			this.Value = value;
		}
		
		[DebuggerHidden]
		public Identity()
		{
			this.Value = default(global::CodeChops.DomainDrivenDesign.DomainModeling.Identities.Uuid);
		}
	}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed override int GetHashCode()
	{
		return this.Id.HasDefaultValue
			? HashCode.Combine(this)
			: this.Id.GetHashCode();
	}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]	
	public bool Equals(Player? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		if (other.GetType() != this.GetType()) return false;
		
		return !this.Id.HasDefaultValue && this.Id.Equals(other.Id);
	}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed override bool Equals(object? obj)
	{
		return obj is Player other 
		       && obj.GetType() == this.GetType() 
		       && this.Equals(other);
	}

	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator ==(Player? left, Player? right)
	{
		if (left is null && right is null) return true;
		if (left is null || right is null) return false;
		return left.Equals(right);
	}
	
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool operator !=(Player? left, Player? right) 
		=> !(left == right);
}

#nullable restore
```

# Miscellaneous
## ToDisplayString
An extension method on `IDomainObject` which can be used when overriding `ToString`.
It creates a display string of the domain object by serializing it and changing the ':' to '='.
The value returned can be configured by providing an anonymous object to customize the string. If omitted, it will create a string of all fields and properties of the class/struct.
For example, to only show the ID of an entity, use: 
```cs 
public string override ToString() => this.ToDisplayString(new { Id = this.Id });
```


## Tuple iterator
Iterates each value of a `ValueTuple`.

## Range iterator
A custom easy-to-use number iterator which uses the `Ranges`-feature (introduced in C# 8).
```cs
foreach (var number in [1..4])
  Console.WriteLine(number);
```
Will write:
```
1
2
3
```


## ValueTuple JSON converter
To enable correct JSON serialization of value tuples, add the following line to Program.cs 
> ```cs
> builder.Services.AddValueTupleJsonSerialization();
> ```

## Global using generator
This packages automatically generates `global usings` for namespaces in this package. Namespaces in this file are often referenced when domain modeling and do not have to be manually referenced anymore.  
