namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

public interface IId : IValueObject
{
	bool HasDefaultValue { get; }
	object GetValue();
}