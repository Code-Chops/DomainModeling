using System.Text.RegularExpressions;

namespace CodeChops.DomainDrivenDesign.DomainModeling.ValueObjects;

/// <summary>
/// A string of type Word.<see cref="Type"/> in Latin characters with case-sensitive comparison (culture invariant).
/// </summary>
public readonly record struct Word : IEnumerable<char>, IHasEmptyInstance<Word>
{
	public override string ToString() => this.Value;

	public static Word Empty { get; } = new(""); 
	
	private string Value { get; }

	public int Length => this.Value.Length;
	
	public static implicit operator string(Word word) => word.Value;
	public static explicit operator Word(string value) => new(value);

	public Word(string value, int? maxLength = 0, Type type = Type.AlphaNumericWithUnderscore, CaseConversion caseConversion = CaseConversion.NoConversion, [CallerMemberName] string? callerName = null)
	{
		var invalidCharacters = type switch
		{
			Type.Alpha						=> Regex.IsMatch(value, "^[a-zA-Z]+$",		RegexOptions.Compiled),
			Type.AlphaWithUnderscore		=> Regex.IsMatch(value, "^[a-zA-Z_]+$",		RegexOptions.Compiled),
			Type.AlphaNumeric				=> Regex.IsMatch(value, "^[a-zA-Z0-9]+$",	RegexOptions.Compiled),
			Type.AlphaNumericWithUnderscore => Regex.IsMatch(value, "^[a-zA-Z0-9_]+$",	RegexOptions.Compiled),
			_								=> throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};

		if (invalidCharacters) throw new ArgumentException($"Invalid characters in {callerName} {type} {nameof(Word)} '{value}'.");
		if (value.Length > maxLength) throw new ArgumentException($"{callerName} {type} {nameof(Word)} '{value}' is longer ({value.Length}) than {nameof(maxLength)} {maxLength}.");

		this.Value = caseConversion switch
		{
			CaseConversion.NoConversion	=> value,
			CaseConversion.AllLowercase	=> value.ToLowerInvariant(),
			CaseConversion.AllUppercase	=> value.ToUpperInvariant(),
			_ => throw new ArgumentOutOfRangeException(nameof(caseConversion), type, null)
		};
	}

	public IEnumerator<char> GetEnumerator() => this.Value.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	
	
	public enum Type
	{
		Alpha,
		AlphaWithUnderscore,
		AlphaNumeric,
		AlphaNumericWithUnderscore,
	}

	public enum CaseConversion
	{
		NoConversion,
		AllLowercase,
		AllUppercase,
	}
}