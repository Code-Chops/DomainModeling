namespace CodeChops.DomainModeling.UnitTests.Validation;

public readonly struct ErrorCodeMock
{
	public static string BoundedContext => "TestContext";
	public string Value { get; }

	public ErrorCodeMock()
	{
		this.Value = "TestErrorCode";
	}
}
