using System.Text.RegularExpressions;
using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

public record RegexGuard : NoOutputGuardBase<RegexGuard, (string Value, Regex Regex), string>, 
	INoOutputGuard<(string Value, Regex Regex)>, 
	IHasExceptionMessage<RegexGuard, string>, 
	IGuard<RegexGuard, string>
{
	public static string GetExceptionMessage(string objectName, string value)
		=> "'{0}' '{1}' is incorrect.";

	public static bool IsValid((string Value, Regex Regex) input)
		=> input.Regex.IsMatch(input.Value);
}

public static class RegexGuardExtensions
{
	public static void GuardRegex(this Validator validator, string value, Regex regex, object? errorCode, Exception? innerException = null)
		=> RegexGuard.Guard(validator, (value, regex), value, errorCode, innerException);
}
