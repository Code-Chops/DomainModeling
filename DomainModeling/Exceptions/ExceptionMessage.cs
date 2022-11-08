using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

/// <summary>
/// Is communicated externally!
/// </summary>
[GenerateValueObject<(string, List<object>)>(minimumValue: 0, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: false, useValidationExceptions: false)]
public readonly ref partial struct ExceptionMessage<TGuard>
	where TGuard : IGuard
{
	private static readonly IErrorCode? ErrorCode_CodeChops_ExceptionMessage_TGuard_OutOfRange;

	public override partial string ToString() => String.Format(this.Message, this.Parameters.ToArray());

	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public string Message => this.Value.Item1;
	
	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public IReadOnlyList<object?> Parameters => this.Value.Item2;

	public ExceptionMessage(string objectName, string message, IEnumerable<object?> parameters)
		: this(value: (message, new object?[] { objectName}.Concat(parameters).ToList())!)
	{
	}
	
	public ExceptionMessage(string objectName, string message, object? parameter)
		: this(value: (message, new[] { objectName, parameter }.ToList())!)
	{	
	}
}
