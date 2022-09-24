namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Core;

public abstract partial class UserException<TException, TParameter> : Exception
	where TException : UserException<TException, TParameter>, IUserException<TException, TParameter>
{
	protected UserException(UserErrorMessage message) 
		: base(message: message.GetMessage())
	{
	}
	
	protected readonly ref struct UserErrorMessage
	{
		private IId<string> ErrorCode { get; }
		private TParameter Parameter { get; }

		public string GetMessage()
		{
			var message = $"{this.ErrorCode}: {TException.ErrorMessage}. {typeof(TParameter).Name}: {this.Parameter}";
			return message;
		}
	
		public UserErrorMessage(IId<string> errorCode, TParameter parameter)
		{
			this.ErrorCode = errorCode;
			this.Parameter = parameter;
		}
	}
}