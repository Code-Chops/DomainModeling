﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// A singleton null ID for singleton entities or testing purposes.
/// </summary>
public sealed record SingletonId<TEntity> : Id<SingletonId<TEntity>, string>
    where TEntity : Entity
{
    public static SingletonId<TEntity> Instance { get; } = new();
    
    private SingletonId()
        : base(typeof(TEntity).Name)
    {
    }
}