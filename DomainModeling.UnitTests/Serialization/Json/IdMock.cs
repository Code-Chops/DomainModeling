using System.Diagnostics.CodeAnalysis;

namespace CodeChops.DomainModeling.UnitTests.Serialization.Json;

public record IdMock : Id<IdMock, uint>
{
	[SetsRequiredMembers]
	public IdMock(uint value)
	{
		this.Value = value;
	}
}
