namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class IndexOutOfRangeException<TCollection> : CustomException, ICustomException
	where TCollection : IEnumerable
{
	public static string ErrorMessage => new($"Index out of range in {typeof(TCollection)}.");
	
	public static dynamic Throw(string errorMessage)
		=> throw new IndexOutOfRangeException(errorMessage);
	
	public static dynamic Throw(int index, string? callerName = null) 
		=> Throw($"{ErrorMessage}. {nameof(index)} = {index}, {nameof(callerName)} = {callerName}");

	protected IndexOutOfRangeException(string errorMessage) 
		: base(errorMessage)
	{
	}
}