namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.Identities;

[GenerateIdentity<string>]
public partial class StringIdGeneration : Entity<StringIdGeneration.Identity>
{
	public StringIdGeneration(Identity id) 
		: base(id)
	{
	}
}
