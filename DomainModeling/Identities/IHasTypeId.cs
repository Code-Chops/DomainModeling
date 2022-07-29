﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// Contains a type discriminator. 
/// </summary>
public interface IHasTypeId<out TId>
	where TId : Id
{
	TId TypeId { get; }
}

/// <summary>
/// Used to get the type ID of runtime types (when the type is not known).
/// </summary>
public interface IHasTypeId
{
	Id GetTypeId();
}