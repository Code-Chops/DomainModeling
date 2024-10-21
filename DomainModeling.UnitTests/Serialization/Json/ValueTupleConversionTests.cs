using System.Text.Json;
using CodeChops.DomainModeling.Serialization;

namespace CodeChops.DomainModeling.UnitTests.Serialization.Json;

public class ValueTupleConversionTests
{
	[Fact]
	public void Serialization_ValueTuple_Is_Correct()
	{
		(int Id, Guid Guid) input = (4, new Guid("00000000-2000-3000-4000-500000000000"));
		var json = JsonSerializer.Serialize(input, JsonSerialization.DefaultOptions);

		Assert.Equal($$"""{"Int32":{{input.Id}},"Guid":"{{input.Guid}}"}""", json);
	}

	[Fact]
	public void Deserialization_ValueTuple_Is_Correct()
	{
		(int Id, Guid Guid) input = (5, new Guid("10000000-2000-3000-4000-500000000000"));
		var json = $$"""{"Int32":{{input.Id}},"Guid":"{{input.Guid}}"}""";

		var output = JsonSerializer.Deserialize<(int Id, Guid Guid)>(json, JsonSerialization.DefaultOptions);

		Assert.Equal(output, input);
	}

}
