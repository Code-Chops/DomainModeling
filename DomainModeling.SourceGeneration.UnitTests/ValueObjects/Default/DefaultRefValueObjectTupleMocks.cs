namespace CodeChops.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<(string, List<object>)>(minimumValue: 0, maximumValue: Int32.MaxValue, generateToString: false, useValidationExceptions: false)]
public readonly ref partial struct DefaultRefValueObjectTupleMock
{
	public override partial string ToString() => String.Format(this.Message, this.Parameters.ToArray());

	public string Message => this.Value.Item1;
	public IReadOnlyList<object?> Parameters => this.Value.Item2;
}
