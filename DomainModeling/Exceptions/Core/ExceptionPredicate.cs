namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Core;

public ref struct ExceptionPredicate
{
	internal bool ShouldTrowException { get; }
	internal string? ArgumentText { get; }
	internal string? CallerMemberName { get; }
	internal string? CallerFilePath { get; }

	internal ExceptionPredicate(bool shouldTrowException, string? argumentText, string? callerMemberName, string? callerFilePath)
	{
		this.ShouldTrowException = shouldTrowException;
		this.ArgumentText = argumentText;
		this.CallerMemberName = callerMemberName;
		this.CallerFilePath = callerFilePath;
	}
}