namespace CodeChops.DomainModeling.Validation.Guards.Core;

public interface IHasExceptionMessage<TSelf, in TMessageParams> : IGuard
	where TSelf : IHasExceptionMessage<TSelf, TMessageParams>
{
	/// <summary>
	/// <em>This message is communicated externally!</em>
	/// </summary>
	public static abstract string GetExceptionMessage(string objectName, TMessageParams parameter);
}
