namespace CodeChops.DomainDrivenDesign.DomainModeling.Attributes;

public sealed class GenerateEntityId : GenerateEntityId<uint>
{
}

[AttributeUsage(AttributeTargets.Class)]
public class GenerateEntityId<TNumber> : Attribute
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	public string IntegralType { get; }
	
	public GenerateEntityId()
	{
		this.IntegralType = typeof(TNumber).FullName;
	}
}