namespace CodeChops.DomainModeling;

/// <summary>
/// A bounded context defines the conditions under which a particular model is defined and applicable (solution space).
/// It separates a model and explicitly draws the boundaries between its pieces (think about namespaces).
/// <list type="bullet">
/// <item>It sets a boundary for the ubiquitous language: ubiquitous language should only be consistent and unified only between a bounded context.</item>
/// <item>It spans across all layers in the onion architecture.</item>
/// <item>It's important to explicitly define the relationships between different bounded contexts using context maps.</item>
/// </list>
/// <para>
/// Note:<br/>
/// A subdomain relates to the problem space, which should be one-to-one to a bounded context.
/// It is often defined by customers and domain experts as it belongs to the problem space.
/// </para>
/// </summary>
public interface IBoundedContext
{
	public static abstract string Name { get; }
}
