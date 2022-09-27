using System.Text.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Identities.Converters.Json;

public class IdentityConversionTests
{
	private static IdentityMock Id { get; } = new(7);
	private const string Json = "7";

	[Fact]
	public void Deserialization_Identity_Is_Correct()
	{
		var id = JsonSerializer.Deserialize<IdentityMock>(Json, JsonSerialization.DefaultOptions)!;

		Assert.Equal(typeof(IdentityMock), id.GetType());
		Assert.Equal(Id.Value, id.Value);
	}
}