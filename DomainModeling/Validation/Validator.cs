using System.Runtime.InteropServices;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public record Validator<TObject> : Validator
	where TObject : IDomainObject
{
	/// <inheritdoc cref="ValidatorMode.Throw"/>
	public static Validator<TObject> Default { get; } = new();
	/// <inheritdoc cref="ValidatorMode.DoNotThrow"/>
	public static Validator<TObject> DoNotThrow() => new(ValidatorMode.DoNotThrow);
	/// <inheritdoc cref="ValidatorMode.Ignore"/>
	public static Validator<TObject> IgnoreWhenInvalid { get; } = new(ValidatorMode.Ignore);

	protected Validator(ValidatorMode mode = ValidatorMode.Throw)
		: base(objectName: typeof(TObject).Name, mode)
	{
	}
}

[StructLayout(LayoutKind.Auto)]
public record Validator
{
	public string ObjectName { get; }

	/// <summary>
	/// Shows the exception that has been occurred. Is null when no validation exception has been triggered.
	/// </summary>
	public IReadOnlyList<ICustomException> CurrentExceptions => this._currentExceptions;
	private readonly List<ICustomException> _currentExceptions;
	
	public bool IsValid => this.CurrentExceptions.Count == 0;

	public ValidatorMode Mode { get; }
	
	public Validator(string objectName, ValidatorMode mode = ValidatorMode.Throw)
	{
		this.ObjectName = objectName;
		this._currentExceptions = new();
		this.Mode = mode;
	}
	
	/// <summary>
	/// Throws the exception when <see cref="Mode"/> is true.
	/// </summary>
	// ReSharper disable twice ExplicitCallerInfoArgument
	public void Throw<TException>(TException exception)
		where TException : ICustomException
		=> this.Throw<TException, int>(exception);

	/// <summary>
	/// Throws the exception when <see cref="Mode"/> is true, otherwise it returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
	public TReturn? Throw<TException, TReturn>(TException exception)
		where TException : ICustomException
	{
		if (this.Mode == ValidatorMode.Throw)
		{
			if (exception is IValidationException validationException) validationException.Throw<int>();
			if (exception is ISystemException systemException) systemException.Throw<int>();
			
			throw new InvalidOperationException($"An unknown exception type was thrown during validation. Exception: {exception.GetType().Name}.");
		}
		
		if (this.Mode != ValidatorMode.Ignore)
			this._currentExceptions.Add(exception);

		return default;
	}
}
