namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IId
{
	bool HasDefaultValue { get; }
	object GetValue();
}