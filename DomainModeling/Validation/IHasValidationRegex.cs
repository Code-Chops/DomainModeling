using System.Text.RegularExpressions;

namespace CodeChops.DomainModeling.Validation;

public interface IHasValidationRegex
{
	protected static abstract Regex ValidationRegex();
}
