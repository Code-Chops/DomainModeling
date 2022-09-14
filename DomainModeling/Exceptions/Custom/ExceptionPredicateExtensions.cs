namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public static class ExceptionPredicateExtensions
{
	public static void Throw<TException, TParameter>(this ExceptionPredicate predicate, TParameter parameter)
		where TException : SystemException, ISystemException<TException, TParameter>, IDomainObject
		where TParameter : class
	{
		if (!predicate.ShouldTrowException) return;
		
		Validate.Throw<TException, TParameter>(parameter);
	}
}