namespace CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

public static class StringExtensions
{
	/// <summary>
	/// Only returns the value with a leading space when the provided value is not null. 
	/// </summary>
	public static string? Write<T>(this T value)
		=> value is null ? null : $" {value}";

	/// <summary>
	/// Only returns the text when the provided value is not null. 
	/// </summary>
	public static string? Write<T>(this T value, string text)
		=> value is null ? null : text;

	/// <summary>
	/// Only returns the text when the provided value is not null. 
	/// </summary>
	public static string? Write<T>(this T value, Func<T, string> text)
		=> value is null ? null : text.Invoke(value);
}
