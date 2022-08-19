namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class NullArgumentException : CustomException<NullArgumentException>, ICustomException<NullArgumentException>
{
	public static string ErrorMessage => nameof(ArgumentNullException);

	public static NullArgumentException Create(string? parameters = null) => new(parameters);
	protected NullArgumentException(string? parameters) : base(parameters) { }
}