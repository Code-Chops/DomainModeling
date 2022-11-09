using System.Runtime.InteropServices;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public record Validator<TObject> : Validator
	where TObject : IDomainObject
{
	// The object is immutable when throwWhenInvalid is true.
	public static Validator<TObject> ThrowWhenInvalid { get; } = new(throwWhenInvalid: true);

	public Validator(bool throwWhenInvalid = true)
		: base(objectName: typeof(TObject).Name, throwWhenInvalid: throwWhenInvalid)
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

	private readonly bool _throwWhenInvalid;
	
	public Validator(string objectName, bool throwWhenInvalid = true)
	{
		this.ObjectName = objectName;
		this._currentExceptions = new();
		this._throwWhenInvalid = throwWhenInvalid;
	}
	
	/// <summary>
	/// Throws the exception when <see cref="_throwWhenInvalid"/> is true.
	/// </summary>
	// ReSharper disable twice ExplicitCallerInfoArgument
	public void Throw<TException>(TException exception)
		where TException : ICustomException
		=> this.Throw<TException, int>(exception);

	/// <summary>
	/// Throws the exception when <see cref="_throwWhenInvalid"/> is true, otherwise it returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
	public TReturn? Throw<TException, TReturn>(TException exception)
		where TException : ICustomException
	{
		if (this._throwWhenInvalid)
		{
			if (exception is IValidationException validationException) validationException.Throw<int>();
			if (exception is ISystemException systemException) systemException.Throw<int>();
			
			throw new InvalidOperationException($"An unknown exception type was thrown during validation. Exception: {exception.GetType().Name}.");
		}
		
		this._currentExceptions.Add(exception);

		return default;
	}
}
