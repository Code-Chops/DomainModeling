using System.Diagnostics.CodeAnalysis;

namespace CodeChops.DomainModeling.UnitTests.Identities.Converters.Json;

public record IdentityMock : Id<IdentityMock, uint>
{
	[SetsRequiredMembers]
	public IdentityMock(uint value)
	{
		this.Value = value;
	}
}
