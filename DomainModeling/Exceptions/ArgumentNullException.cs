using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class ArgumentNullException : System.ArgumentNullException, ICustomException<ArgumentNullException>
{
	public static string ErrorMessage => nameof(ArgumentNullException);

	public static ArgumentNullException Create(string errorMessage) => new(errorMessage);
	protected ArgumentNullException(string errorMessage) : base(errorMessage) { }
}