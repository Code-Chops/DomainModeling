using System.Text.RegularExpressions;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation;

public interface IHasValidationRegex
{
	protected static abstract Regex ValidationRegex();
}
