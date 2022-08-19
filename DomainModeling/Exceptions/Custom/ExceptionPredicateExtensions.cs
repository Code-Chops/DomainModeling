namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public static class ExceptionPredicateExtensions
{
	public static void Throw<TException>(this ExceptionPredicate predicate)
		where TException : Exception, ICustomException
	{
		if (!predicate.ShouldTrowException) return;

		throw TException.Throw(errorMessage: $"{TException.ErrorMessage}. Argument: {predicate.ArgumentText ?? "<unknown>"}.");
	}
}