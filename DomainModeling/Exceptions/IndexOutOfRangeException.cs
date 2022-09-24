namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class IndexOutOfRangeException<TCollection> : SystemException<IndexOutOfRangeException<TCollection>, object>, ISystemException<IndexOutOfRangeException<TCollection>, object>
{
	public static string ErrorMessage => new($"Index out of range in {typeof(TCollection).Name}.");

	public IndexOutOfRangeException(object parameter) 
		: base(parameter)
	{
	}

	public static IndexOutOfRangeException<TCollection> Create(object parameter)
		=> new(parameter);
}