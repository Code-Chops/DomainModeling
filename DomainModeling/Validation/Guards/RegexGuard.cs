using System.Text.RegularExpressions;
using CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards.Core;

namespace CodeChops.DomainDrivenDesign.DomainModeling.Validation.Guards;

public record RegexGuard : NoOutputGuardBase<RegexGuard, (string Value, string Pattern), string>, 
	INoOutputGuard<(string Value, string Pattern)>, 
	IHasExceptionMessage<RegexGuard, string>, IGuard<RegexGuard, string>
{
	public static string GetMessage(string objectName, string value)
		=> "{0} {1} is incorrect.";

	public static bool IsValid((string Value, string Pattern) input)
		=> Regex.IsMatch(input.Value, input.Pattern, RegexOptions.Compiled);
}

public static class RegexGuardExtensions
{
	public static Validator GuardRegex(this Validator validator, string value, string pattern, IErrorCode? errorCode, Exception? innerException = null)
		=> RegexGuard.Guard(validator, (value, pattern), value, errorCode, innerException);
}
