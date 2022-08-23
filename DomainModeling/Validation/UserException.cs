namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public class UserException<TException> : Exception
	where TException : UserException<TException>, IUserException<TException>
{
	protected UserException(UserErrorMessage message) 
		: base(message: message.GetMessage())
	{
	}
	
	protected readonly ref struct UserErrorMessage
	{
		private Id<string> ErrorCode { get; }
		private string ParameterName { get; }

		public string GetMessage()
		{
			var message = $"{this.ErrorCode}: {TException.ErrorMessage}. Parameter: {this.ParameterName}";
			return message;
		}
	
		public UserErrorMessage(Id<string> errorCode, string parameterName)
		{
			this.ErrorCode = errorCode;
			this.ParameterName = parameterName;
		}
	}
}