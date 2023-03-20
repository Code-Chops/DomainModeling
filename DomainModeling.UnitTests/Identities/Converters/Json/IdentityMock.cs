using System.Diagnostics.CodeAnalysis;

namespace CodeChops.DomainModeling.UnitTests.Identities.Converters.Json;

public record IdentityMock : Id<IdentityMock, uint>
{
	[SetsRequiredMembers]
#pragma warning disable CS8618
	public IdentityMock(uint value)
#pragma warning restore CS8618
	{
		this.Value = value;
	}
}
