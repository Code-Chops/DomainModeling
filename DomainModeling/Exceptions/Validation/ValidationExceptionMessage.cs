namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// Is communicated externally!
/// </summary>
[GenerateValueObject<(string, ImmutableList<object>)>(minimumValue: 0, maximumValue: Int32.MaxValue, addCustomValidation: false, generateToString: false, useValidationExceptions: false)]
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
	public ImmutableList<object?> Parameters => this.Value.Item2!;

	public ValidationExceptionMessage(string objectName, string message, IEnumerable<object?> parameters)
		: this((message, new object?[] { objectName}.Concat(parameters).ToImmutableList())!)
	{
	}
	
	public ValidationExceptionMessage(string objectName, string message, object? parameter)
		: this((message, new[] { objectName, parameter }.ToImmutableList())!)
	{	
	}
	
	/// <summary>
	/// Used for deserialization.
	/// </summary>
	internal ValidationExceptionMessage(string message, IEnumerable<object?> parameters)
		: this((message, parameters.ToImmutableList())!)
	{	
	}
}
