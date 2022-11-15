namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// Is communicated externally!
/// </summary>
[GenerateValueObject<(string, List<object>)>(minimumValue: 0, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: false, useValidationExceptions: false)]
public readonly partial record struct ValidationExceptionMessage
{
	public override partial string ToString() => String.Format(this.Message, this.Parameters.ToArray());

	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public string Message => this.Value.Item1;
	
	/// <summary>
	/// Is communicated externally!
	/// </summary>
	public IReadOnlyList<object?> Parameters => this.Value.Item2;

	public ValidationExceptionMessage(string objectName, string message, IEnumerable<object?> parameters)
		: this(((string, List<object>))(message, new object?[] { objectName}.Concat(parameters).ToList())!)
	{
	}
	
	public ValidationExceptionMessage(string objectName, string message, object? parameter)
		: this(((string, List<object>))(message, new[] { objectName, parameter }.ToList())!)
	{	
	}
}
