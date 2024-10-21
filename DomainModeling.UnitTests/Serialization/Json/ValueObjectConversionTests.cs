using System.Text.Json;
using CodeChops.DomainModeling.Serialization;

namespace CodeChops.DomainModeling.UnitTests.Serialization.Json;

public class ValueObjectConversionTests
{
	[Fact]
	public void Serialization_Identity_Is_Correct()
	{
		var input = new IdMock(6);
		var json = JsonSerializer.Serialize(input, JsonSerialization.DefaultOptions);

		Assert.Equal($"{input.Value}", json);
	}

	[Fact]
	public void Deserialization_Identity_Is_Correct()
	{
		var input = new IdMock(7);
		var json = $"{input.Value}";

		var output = JsonSerializer.Deserialize<IdMock>(json, JsonSerialization.DefaultOptions)!;

		Assert.Equal(typeof(IdMock), output.GetType());
		Assert.Equal(output.Value, input.Value);
	}

	[Fact]
	public void Serialization_ValueObject_Is_Correct()
	{
		var input = new ValueObjectMock(9);
		var json = JsonSerializer.Serialize(input, JsonSerialization.DefaultOptions);

		Assert.Equal($"{(int)input}", json);
	}

	[Fact]
	public void Deserialization_ValueObject_Is_Correct()
	{
		var input = new ValueObjectMock(8);
		var json = $"{(int)input}";

		var output = JsonSerializer.Deserialize<ValueObjectMock>(json, JsonSerialization.DefaultOptions)!;

		Assert.Equal(typeof(ValueObjectMock), output.GetType());
		Assert.Equal((int)output, (int)input);
	}
}
