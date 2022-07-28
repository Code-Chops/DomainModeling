namespace CodeChops.DomainDrivenDesign.DomainModeling.Helpers;

public static class ExceptionHelpers
{
    public static ArgumentNullException ArgumentNullException<TArgument>()                  
        => new(typeof(TArgument).Name);
    
    public static KeyNotFoundException KeyNotFoundException<TCollection, TKey>(TKey key)   
        => new($"Key {key} not found in {typeof(TCollection).BaseType!.Name} {typeof(TCollection).Name}.");
}