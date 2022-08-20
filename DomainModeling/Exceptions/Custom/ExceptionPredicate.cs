namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

public ref struct ExceptionPredicate
{
	internal bool ShouldTrowException { get; }
	internal string? ArgumentText { get; }

	internal ExceptionPredicate(bool shouldTrowException, string? argumentText)
	{
		this.ShouldTrowException = shouldTrowException;
		this.ArgumentText = argumentText;
	}
}