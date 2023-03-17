namespace CodeChops.DomainModeling.UnitTests.Identities.Converters.Json;

public record IdentityMock : Id<IdentityMock, uint>
{
	public IdentityMock(uint value) 
		: base(value)
	{
	}

	public IdentityMock()
	{
	}
}
