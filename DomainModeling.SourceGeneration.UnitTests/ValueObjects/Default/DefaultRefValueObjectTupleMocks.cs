using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.SourceGeneration.UnitTests.ValueObjects.Default;

[GenerateValueObject<(string, List<object>)>(minimumValue: 0, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: false)]
public readonly ref partial struct DefaultRefValueObjectTupleMock<TGuard>
	where TGuard : IGuard
{
	private static readonly IErrorCode? ErrorCode_CodeChops_ExceptionMessage_TGuard_OutOfRange;

	public override partial string ToString() => String.Format(this.Message, this.Parameters.ToArray());

	public string Message => this.Value.Item1;
	public IReadOnlyList<object?> Parameters => this.Value.Item2;
}
