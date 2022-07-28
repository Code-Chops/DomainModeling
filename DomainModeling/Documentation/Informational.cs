namespace CodeChops.DomainDrivenDesign.DomainModeling.Documentation;

/// <summary>
/// This enum is just used for domain modeling reference and not for any logic. 
/// </summary>
public enum Informational
{
	/// <summary>
	/// It defines the conditions under which a particular model is defined and applicable (solution space).
	/// It separates a model and explicitly draws the boundaries between its pieces (think about namespaces).
	/// <list type="bullet">
	/// <item>It sets a boundary for the ubiquitous language: ubiquitous language should only be consistent and unified only between a bounded context.</item>
	/// <item>It spans across all layers in the onion architecture.</item>
	/// <item>It's important to explicitly define the relationships between different bounded contexts using context maps.</item>
	/// </list>
	/// </summary>
	BoundedContext,
	
	/// <summary>
	/// Relates to the problem space. Should be one-to-one to <see cref="BoundedContext"/>.
	/// Often defined by customers and domain experts as it belongs to the problem space.
	/// </summary>
	Subdomain,
}