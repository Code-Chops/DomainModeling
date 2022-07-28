using CodeChops.DomainDrivenDesign.DomainModeling;

// ReSharper disable once CheckNamespace
namespace CodeChops.GenericMath;

/// <summary>
/// Make Number implement IValueObject (is not possible in the original package because it will lead to circular references).
/// </summary>
/// <typeparam name="T">Integral type</typeparam>
public readonly partial record struct Number<T> : IValueObject
	where T : struct, IComparable<T>, IEquatable<T>, IConvertible;