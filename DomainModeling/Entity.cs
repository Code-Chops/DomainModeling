namespace CodeChops.DomainModeling;

/// <inheritdoc cref="IEntity"/>
[GenerateIdentity]
public abstract partial class Entity : IEntity
{
	public override string ToString() => this.ToDisplayString(new { this.Id });
}
