namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

public interface IHasExceptionMessage<TSelf, in TMessageParams> : IGuard
	where TSelf : IHasExceptionMessage<TSelf, TMessageParams>
{
	/// <summary>
	/// <em>This message is communicated externally!</em>
	/// </summary>
	public static abstract string GetMessage(string objectName, TMessageParams parameter);
}
