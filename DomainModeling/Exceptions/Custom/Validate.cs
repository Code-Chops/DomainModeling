namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public static class Validate
{
	/// <summary>
	/// Throws a custom defined exception when <paramref name="shouldThrowException"/> is true.
	/// </summary>
	/// <param name="shouldThrowException">If true, throw exception. Otherwise, don't do anything.</param>
	/// <param name="argumentText">The name of the argument. Don't provide this value!</param>
	public static ExceptionPredicate If(bool shouldThrowException, [CallerArgumentExpression("shouldThrowException")] string? argumentText = null) 
		=> new(shouldThrowException, argumentText);

	[DoesNotReturn]
	public static void Throw<TException>(string? argumentText = null, object? parameters = null)
		where TException : Exception, ICustomException<TException>
	{
		var message = EasyStringHelper.ToExceptionString(typeof(TException), TException.ErrorMessage, parameters, argumentText); 
		var exception = TException.Create(message);
		throw exception;
	}
}