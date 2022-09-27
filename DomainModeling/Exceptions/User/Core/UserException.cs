namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.User.Core;

// ReSharper disable once UnusedTypeParameter
public abstract record UserException<TSelf>(string Message) : IUserException
	where TSelf : IUserException
{
	public void Throw() => throw new Exception(this.Message);
	
	private class Exception : global::System.Exception
	{
		internal Exception(string message) 
			: base(message: message)
		{
		}
	}
}