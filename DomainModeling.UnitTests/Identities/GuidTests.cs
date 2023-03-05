using CodeChops.DomainModeling.Exceptions.System;
using CodeChops.DomainModeling.Validation.Guards;

namespace CodeChops.DomainModeling.UnitTests.Identities;

public class GuidTests
{
	[Theory]
	[InlineData("2FD110A01D304B4593B4D44680DE152C",		true)]
	[InlineData(null,									true)]
	[InlineData("a3af8461ae00492fb5b5e10f4243025c",		false)]
	[InlineData("{C1D27F19F8B84EDD8C2C068882AFA7FB}",	false)]
	[InlineData("95BDDA09-AB70-4F83-AABD-0219EA82F516", false)]
	public void Guid_Should_Be_Valid(string? value, bool expectedToBeValid)
	{
		if (!expectedToBeValid) Assert.Throws<CustomSystemException<RegexGuard>>(CreateGuid);

		object CreateGuid() => value is null ? new Uuid() : new Uuid(value);
	}

	[Fact]
	public void IdenticalGuids_ShouldBe_Equal()
	{
		var guid1 = new Uuid("2FD110A01D304B4593B4D44680DE152C");
		var guid2 = new Uuid("2FD110A01D304B4593B4D44680DE152C");
		
		Assert.True(guid1 == guid2);
		Assert.True(guid1.Equals(guid2));
	}
}
