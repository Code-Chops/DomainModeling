namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public static class ExceptionPredicateExtensions
{
	public static void Throw<TException>(this ExceptionPredicate predicate, string? parameterName = null)
		where TException : SystemException, ISystemException<TException>, IDomainObject
	{
		if (!predicate.ShouldTrowException) return;
		
		parameterName ??= predicate.ArgumentText ?? Path.GetFileNameWithoutExtension(predicate.CallerFilePath) ?? "<unknown>";
		
		Validate.Throw<TException>(info: parameterName);
	}
}