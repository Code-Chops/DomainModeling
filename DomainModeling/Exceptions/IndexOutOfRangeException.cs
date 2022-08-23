namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class IndexOutOfRangeException<TCollection> : SystemException<IndexOutOfRangeException<TCollection>>, ISystemException<IndexOutOfRangeException<TCollection>>
	where TCollection : IEnumerable
{
	public static string ErrorMessage => new($"Index out of range in {typeof(TCollection)}.");

	public IndexOutOfRangeException(object parameters) 
		: base(parameters)
	{
	}

	public static IndexOutOfRangeException<TCollection> Create(object parameters) 
		=> new(parameters);
}