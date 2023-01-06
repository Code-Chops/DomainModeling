using System.Diagnostics;

namespace CodeChops.DomainModeling.Validation;

/// <summary>
/// <para>
/// Provides a way to easily use validation in different <see cref="ValidatorMode"/>s and enables the usage of <see cref="Guards.Core.IGuard"/>s.
/// </para>
/// <para></para>
/// </summary>
public record Validator
{
	public static class Get<TObject>
	{
		/// <inheritdoc cref="ValidatorMode.Default"/>
		public static Validator Default { get; } = new(objectName: typeof(TObject).Name);
		/// <inheritdoc cref="ValidatorMode.DoNotThrow"/>
		public static Validator DoNotThrow() => new(objectName: typeof(TObject).Name, ValidatorMode.DoNotThrow);
		/// <inheritdoc cref="ValidatorMode.Oblivious"/>
		public static Validator Oblivious { get; } = new(objectName: typeof(TObject).Name, ValidatorMode.Oblivious);
	}
	
	public string ObjectName { get; }

	/// <summary>
	/// Shows the exception that has been occurred. Is null when no validation exception has been triggered.
	/// </summary>
	public IReadOnlyList<ICustomException> CurrentExceptions => this._currentExceptions;
	private readonly List<ICustomException> _currentExceptions;
	
	public bool IsValid => this.CurrentExceptions.Count == 0;

	public ValidatorMode Mode { get; }

	/// <summary>
	/// <para><b>Use Validator.<see cref="Validator.Get{TObject}"/> to get a validator.</b></para>
	/// <para>Only use this constructor if the object to be validated is a ref struct.</para>
	/// </summary>
	/// <param name="objectName"></param>
	/// <param name="mode"></param>
	public Validator(string objectName, ValidatorMode mode = ValidatorMode.Default)
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
		switch (this.Mode)
		{
			case ValidatorMode.Oblivious:
				return default;
			case ValidatorMode.Default:
				{
					if (exception is IValidationException validationException) validationException.Throw<int>();
					if (exception is ISystemException systemException) systemException.Throw<int>();
			
					throw new InvalidOperationException($"An unknown exception type was thrown during validation. Exception: {exception.GetType().Name}.");
				}
			case ValidatorMode.DoNotThrow:
				this._currentExceptions.Add(exception);
				break;
			default:
				throw new UnreachableException($"{nameof(ValidatorMode)} has an unexpected value of {this.Mode}. This should not happen!");
		}

		return default;
	}
}
