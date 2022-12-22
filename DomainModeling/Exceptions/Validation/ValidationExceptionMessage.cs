namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Validation;

/// <summary>
/// A validation message is communicated externally and contains a string message and parameters (which can be used for String.Format).
/// </summary>
/// Outcommented attribute and added source generated content in ValidationExceptionMessage_temp file, because generic attributes still don't work in Blazor: https://github.com/dotnet/runtime/issues/77047
//[GenerateValueObject<(string, ImmutableList<object>)>(minimumValue: 0, maximumValue: Int32.MaxValue, generateToString: false, useValidationExceptions: false)]
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
