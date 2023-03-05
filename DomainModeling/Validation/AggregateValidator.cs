namespace CodeChops.DomainModeling.Validation;

/// <summary>
/// <para>Collects validation exceptions when a guard invalidates and throws when it gets disposed (except when <see cref="ThrowWhenDisposed"/> is enabled).</para>
/// <para>It throws an <see cref="ValidationAggregateException"/> when it contains multiple exceptions. If it contains only 1 exception, it throws the exception itself.</para>
/// <para>In case a guard invalidates and should return an object, it returns the default.</para>
/// <para>This validator is mutable.</para>
/// <remarks>Use with a using statement or declaration!</remarks>
/// </summary>
public record AggregateValidator : Validator, IDisposable
{
	public override bool IsValid => this.CurrentExceptions.Count == 0;
	public bool ThrowWhenDisposed { get; set; }
	
	public IReadOnlyList<ValidationException> CurrentExceptions => this._currentExceptions;
	private readonly List<ValidationException> _currentExceptions;
	
	internal AggregateValidator(string objectName, bool throwWhenDisposed, IEnumerable<ValidationException>? exceptions = null) 
		: base(objectName)
	{
		this.ThrowWhenDisposed = throwWhenDisposed;
		this._currentExceptions = exceptions as List<ValidationException> ?? (exceptions?.ToList() ?? new());
	}
	
	/// <summary>
	/// Collects the provided <see cref="ValidationException"/>. Does not throw.
	/// </summary>
	public override TReturn Throw<TException, TReturn>(TException exception)
	{
		if (exception is ValidationException validationException)
			this._currentExceptions.Add(validationException);
		else
			exception.Throw(); // If something other than a ValidationException is provided, we shouldn't collect it. Throw the exception itself.

		return default!;
	}

	public void Dispose()
	{
		if (this.IsValid || !this.ThrowWhenDisposed)
			return;

		if (this._currentExceptions.Count == 1)
			this.Throw(this._currentExceptions[0]);

		var aggregateException = new ValidationAggregateException(this._currentExceptions);
		aggregateException.Throw();
	}
}
