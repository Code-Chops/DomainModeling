namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public static class ExceptionPredicateExtensions
{
	public static void Throw<TException>(this ExceptionPredicate predicate, object? parameters = null)
		where TException : Exception, ICustomException<TException>, IDomainObject
	{
		if (!predicate.ShouldTrowException) return;
		
		Validate.Throw<TException>(predicate.ArgumentText, parameters);
	}
}