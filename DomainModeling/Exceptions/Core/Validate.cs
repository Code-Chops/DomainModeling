namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Core;

public static class Validate
{
	/// <summary>
	/// Throws a system exception when <paramref name="shouldThrowException"/> is true.
	/// </summary>
	/// <param name="shouldThrowException">If true, throw exception. Otherwise, don't do anything.</param>
	/// <param name="argumentText">The name of the argument. Don't provide this value!</param>
	public static ExceptionPredicate If(
		bool shouldThrowException, 
		[CallerArgumentExpression("shouldThrowException")] string? argumentText = null, 
		[CallerMemberName] string? callerMemberName = null, 
		[CallerFilePath] string? callerFilePath = null) 
			=> new(shouldThrowException, argumentText, callerMemberName: callerMemberName, callerFilePath: callerFilePath);
	
	[DoesNotReturn]
	public static void Throw<TException, TParameter>(TParameter parameter)
		where TException : SystemException, ISystemException<TException, TParameter>
	{
		throw TException.Create(parameter);
	}
}