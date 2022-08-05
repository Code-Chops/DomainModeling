﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has instance of type <typeparamref name="T"/>. Be sure that the empty instance is immutable!
/// </summary>
/// <typeparam name="T">Type of the immutable empty instance</typeparam>
public interface IHasEmptyInstance<out T>
	where T : class
{
	static abstract T Empty { get; }
}