namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public ref struct ExceptionPredicate
{
	public bool ShouldTrowException { get; }
	public string? ArgumentText { get; }

	public ExceptionPredicate(bool shouldTrowException, string? argumentText)
	{
		this.ShouldTrowException = shouldTrowException;
		this.ArgumentText = argumentText;
	}
}