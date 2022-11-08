namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

public interface IOutputGuard<in TInput, TOutput> : IGuard
{
	public static abstract bool IsValid(TInput input, out TOutput output);
}
