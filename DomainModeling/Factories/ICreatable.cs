namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TObject"/>
/// by providing parameter <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="TObject">The type to be created.</typeparam>
public interface ICreatable<out TObject, in T>
{
	static abstract TObject CreateInstance(T parameter);
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TObject"/>
/// by providing parameters <typeparamref name="T1"/> and <typeparamref name="T2"/>.
/// </summary>
/// <typeparam name="TObject">The type to be created.</typeparam>
public interface ICreatable<out TObject, in T1, in T2>
{
	static abstract TObject CreateInstance(T1 parameter1, T2 parameter2);
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TObject"/>
/// by providing parameters <typeparamref name="T1"/>, <typeparamref name="T2"/> and <typeparamref name="T3"/>.
/// </summary>
/// <typeparam name="TObject">The type to be created.</typeparam>
public interface ICreatable<out TObject, in T1, in T2, in T3>
{
	static abstract TObject CreateInstance(T1 parameter1, T2 parameter2, T3 parameter3);
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TObject"/>
/// by providing parameters <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/> and <typeparamref name="T4"/>.
/// </summary>
/// <typeparam name="TObject">The type to be created.</typeparam>
public interface ICreatable<out TObject, in T1, in T2, in T3, in T4>
{
	static abstract TObject CreateInstance(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
}
