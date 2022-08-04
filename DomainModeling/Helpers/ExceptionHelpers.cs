namespace CodeChops.DomainDrivenDesign.DomainModeling.Helpers;

public static class ExceptionHelpers
{
    public static ArgumentNullException ArgumentNullException<TArgument>()                  
        => new(typeof(TArgument).Name);
    
    public static KeyNotFoundException KeyNotFoundException<TDictionary, TKey>(TKey key)
        where TDictionary : IEnumerable
        => new($"Key {key} not found in {typeof(TDictionary).BaseType!.Name} {typeof(TDictionary).Name}.");
    
    public static IndexOutOfRangeException IndexOutOfRangeException<TCollection>(int index)
        where TCollection : IEnumerable
        => new($"Index {index} out of range in {typeof(TCollection).BaseType!.Name} {typeof(TCollection).Name}.");
}