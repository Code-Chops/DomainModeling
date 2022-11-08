using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Validation;

public readonly struct ErrorCodeMock : IErrorCode
{
	public static string BoundedContext => "TestContext";
	public string Value { get; }

	public ErrorCodeMock()
	{
		this.Value = "TestErrorCode";
	}
}
