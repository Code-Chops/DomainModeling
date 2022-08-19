namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public static class ExceptionPredicateExtensions
{
	public static void Throw<TException>(this ExceptionPredicate predicate, object? parameters = null)
		where TException : Exception, ICustomException<TException>, IDomainObject
	{
		if (!predicate.ShouldTrowException) return;
		
		var extraInfo = predicate.ArgumentText is null ? null : $" argument: {predicate.ArgumentText}.";
		var parametersText = parameters is null ? null : $"Info: {EasyStringHelper.ToEasyString(typeof(TException), parameters, extraInfo)}.";
		var message = $"{TException.ErrorMessage}.{parametersText}";
		
		var exception = TException.Create(message);
		throw exception;
	}
}