namespace CodeChops.DomainModeling.UnitTests.Extensions;

public class DomainObjectExtensionsTests
{
	private class Mock : IDomainObject
	{
		public override string ToString() => this.ToDisplayString(new { A, this.B, C, D = D() }, "ExtraInfo");
		
		public static string A { get; } = "1";
		private int B { get; } = 2;
		internal const int C = 3;
		protected static int D() => 4;
	}

	[Fact]
	public void ToString_IsCorrect()
	{
		var text = new Mock().ToString();
		
		Assert.Equal("Mock (ExtraInfo) { A = 1, B = 2, C = 3, D = 4 }", text);
	}
}
