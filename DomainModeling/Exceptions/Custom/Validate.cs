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
}