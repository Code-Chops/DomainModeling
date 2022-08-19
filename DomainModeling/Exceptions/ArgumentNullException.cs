using CodeChops.DomainDrivenDesign.DomainModeling.Exceptions.Custom;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Exceptions;

public class NullArgumentException : ArgumentNullException, ICustomException<NullArgumentException>
{
	public static string ErrorMessage => nameof(ArgumentNullException);

	public static NullArgumentException Create(string errorMessage) => new(errorMessage);
	protected NullArgumentException(string errorMessage) : base(errorMessage) { }
}