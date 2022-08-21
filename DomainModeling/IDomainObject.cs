﻿namespace CodeChops.DomainDrivenDesign.DomainModeling;

/// <summary>
/// An object in the domain model.
/// </summary>
public interface IDomainObject
{
	/// <summary>
	/// Use <see cref="DomainObjectExtensions.ToEasyString"/> for easy string display.
	/// </summary>
	string ToString();
}