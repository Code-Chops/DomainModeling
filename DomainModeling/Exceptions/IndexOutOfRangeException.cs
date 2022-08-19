namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class IndexOutOfRangeException<TCollection> : CustomException<IndexOutOfRangeException<TCollection>>, ICustomException<IndexOutOfRangeException<TCollection>>
	where TCollection : IEnumerable
{
	public static string ErrorMessage => new($"Index out of range in {typeof(TCollection)}.");
	
	public static IndexOutOfRangeException<TCollection> Create(int index, string? callerName = null) 
		=> new($"{ErrorMessage}. {nameof(index)} = {index}, {nameof(callerName)} = {callerName}");

	public static IndexOutOfRangeException<TCollection> Create(object? parameters = null) => new(parameters);
	private IndexOutOfRangeException(object? parameters) : base(parameters) { }
}