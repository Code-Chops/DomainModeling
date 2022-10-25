namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User.Core;

/// <summary>
/// <inheritdoc cref="IUserException"/>
/// </summary>
/// <typeparam name="TSelf">The type of the concrete user exception.</typeparam>
// ReSharper disable once UnusedTypeParameter
public abstract record UserException<TSelf> : IUserException
	where TSelf : IUserException
{
	/// <summary>
	/// The exception message.
	/// </summary>
	public string Message { get; }
	
	/// <summary>
	/// <para>This code can be communicated externally and can be used by clients to provided resource translations to the end-user.</para>
	/// <para>Is equal to the name of the concrete user exception by default.</para>
	/// </summary>
	public string? ErrorCode { get; }

	protected UserException(string message, string? errorCode = null)
	{
		this.Message = message;
		this.ErrorCode = errorCode ?? typeof(TSelf).Name;
	}

	/// <summary>
	/// Throws the exception. Returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
	[DoesNotReturn] public TReturn Throw<TReturn>() => throw new Exception(this.Message, this.ErrorCode);
	
	/// <summary>
	/// Throws the exception.
	/// </summary>
	[DoesNotReturn] public void Throw() => throw new Exception(this.Message, this.ErrorCode);
	
	/// <summary>
	/// The real System.Exception is wrapped so records can be used for exceptions (records provide less boilerplate).
	/// </summary>
	private class Exception : global::System.Exception, IUserException
	{
		public string? ErrorCode { get; }
		
		internal Exception(string message, string? errorCode) 
			: base(message: message)
		{
			this.ErrorCode = errorCode;
		}
	}
}
