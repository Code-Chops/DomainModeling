namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User.Core;

/// <summary>
/// <inheritdoc cref="IUserException"/>
/// </summary>
/// <param name="Message">The exception message.</param>
/// <typeparam name="TSelf">The type of the concrete user exception.</typeparam>
/// <param name="ErrorCode">
/// <para>This code can be communicated externally and can be used by clients to provided resource translations to the end-user.</para>
/// <para>Is equal to the name of the concrete user exception by default.</para>
/// </param>
// ReSharper disable once UnusedTypeParameter
public abstract record UserException<TSelf>(string Message, string ErrorCode = nameof(TSelf)) : IUserException
	where TSelf : IUserException
{
	/// <summary>
	/// Throws the exception. Returns the default of the provided return type (so it can be used in an inline if-else).
	/// </summary>
	[DoesNotReturn] public TReturn Throw<TReturn>() => throw new Exception(this.Message);
	
	/// <summary>
	/// Throws the exception.
	/// </summary>
	[DoesNotReturn] public void Throw() => throw new Exception(this.Message);
	
	/// <summary>
	/// The real System.Exception is wrapped so records can be used for exceptions (records provide less boilerplate).
	/// </summary>
	private class Exception : global::System.Exception
	{
		internal Exception(string message) 
			: base(message: message)
		{
		}
	}
}