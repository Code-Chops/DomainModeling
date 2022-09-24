namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public class OutOfRangeUserException<TCollection, TValue> : UserException<OutOfRangeUserException<TCollection, TValue>, TValue>, IUserException<OutOfRangeUserException<TCollection, TValue>, TValue> 
{
	public static string ErrorMessage => new($"{typeof(TValue).Name} out of range in {typeof(TCollection).Name}.");
	
	public static OutOfRangeUserException<TCollection, TValue> Create(IId<string> errorCode, TValue value) 
		=> new(errorCode, value);

	protected OutOfRangeUserException(IId<string> errorCode, TValue parameter) 
		: base(new(errorCode, parameter))
	{
	}
}