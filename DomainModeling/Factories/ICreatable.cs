namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a factory to create objects of type <typeparamref name="T"/>
/// by providing <typeparamref name="TParameter"/>.
/// </summary>
/// <typeparam name="T">The type to be created.</typeparam>
public interface ICreatable<out T, in TParameter>
{
	static abstract T Create(TParameter parameter);
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="T"/>
/// by providing <typeparamref name="TParameter1"/> and <typeparamref name="TParameter2"/>.
/// </summary>
/// <typeparam name="T">The type to be created.</typeparam>
public interface ICreatable<out T, in TParameter1, in TParameter2>
{
	static abstract T Create(TParameter1 parameter1, TParameter2 parameter2);
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="T"/>
/// by providing <typeparamref name="TParameter1"/>, <typeparamref name="TParameter2"/> and <typeparamref name="TParameter3"/>.
/// </summary>
/// <typeparam name="T">The type to be created.</typeparam>
public interface ICreatable<out T, in TParameter1, in TParameter2, in TParameter3>
{
	static abstract T Create(TParameter1 parameter1, TParameter2 parameter2, TParameter3 parameter3);
}