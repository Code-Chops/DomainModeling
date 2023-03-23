namespace CodeChops.DomainModeling.Validation;

/// <summary>
/// <para>Collects exceptions when a guard invalidates and throws when it gets disposed (except when <see cref="ThrowWhenDisposed"/> is enabled).</para>
/// <para>It throws an <see cref="CustomAggregateException"/> when it contains multiple exceptions. If it contains only 1 exception, it throws the exception itself.</para>
/// <para>In case a guard invalidates and should return an object, it returns the default.</para>
/// <para>This validator is mutable.</para>
/// <remarks>Use with a using statement or declaration!</remarks>
/// </summary>
public record AggregateValidator : Validator, IDisposable
{
	public override bool HasException => this.CurrentExceptions.Count == 0;
	
	/// <summary>
	/// Throw when the validator is disposed and exceptions occurred. Default: true.
	/// </summary>
	public bool ThrowWhenDisposed { get; }
	
	public IReadOnlyList<CustomException> CurrentExceptions => this._currentExceptions;
	private readonly List<CustomException> _currentExceptions;
	
	internal AggregateValidator(string objectName, bool throwWhenDisposed, IEnumerable<CustomException>? exceptions = null) 
		: base(objectName)
	{
		this.ThrowWhenDisposed = throwWhenDisposed;
		this._currentExceptions = exceptions as List<CustomException> ?? (exceptions?.ToList() ?? new());
	}
	
	/// <summary>
	/// Collects the provided <see cref="CustomException"/>. Does not throw.
	/// </summary>
	public override TReturn Throw<TException, TReturn>(TException exception)
	{
		this._currentExceptions.Add(exception);

		return default!;
	}

	public void Dispose()
	{
		if (this.HasException || !this.ThrowWhenDisposed)
			return;

		if (this._currentExceptions.Count == 1)
			this.Throw(this._currentExceptions[0]);

		var aggregateException = new CustomAggregateException(this._currentExceptions);
		aggregateException.Throw();
	}
}
