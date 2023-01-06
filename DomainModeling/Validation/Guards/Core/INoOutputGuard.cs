namespace CodeChops.DomainModeling.Validation.Guards.Core;

public interface INoOutputGuard<in TInput> : IGuard
{
	public static abstract bool IsValid(TInput input);
}
