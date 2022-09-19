using System.Text.Json;
using CodeChops.DomainDrivenDesign.DomainModeling.Identities.Converters.Json.Identities;

namespace CodeChops.DomainDrivenDesign.DomainModeling.UnitTests.Identities.Converters.Json;

public class IdentityConversionTests
{
	private static IdentityMock Id { get; } = new(7);
	private const string Json = "7";

	private JsonSerializerOptions JsonSerializerOptions { get; }

	public IdentityConversionTests()
	{
		this.JsonSerializerOptions = new()
		{
			WriteIndented = false, 
			Converters = { new IdentityJsonConverterFactory() }
		};
	}
	
	[Fact]
	public void Deserialization_Identity_Is_Correct()
	{
		var id = JsonSerializer.Deserialize<IdentityMock>(Json, this.JsonSerializerOptions)!;

		Assert.Equal(typeof(IdentityMock), id.GetType());
		Assert.Equal(Id.Value, id.Value);
	}
}