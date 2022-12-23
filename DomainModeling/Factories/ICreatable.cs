namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TSelf"/>.
/// </summary>
/// <typeparam name="TSelf">The type to be created.</typeparam>
public interface ICreatable<out TSelf>
	where TSelf : ICreatable<TSelf>, IDomainObject
{
	static abstract TSelf Create(Validator? validator = null);

	public static bool TryCreate([NotNullWhen(true)] out TSelf createdObject) 
		=> TryCreate(out createdObject, out _);

	public static bool TryCreate([NotNullWhen(true)] out TSelf createdObject, out Validator validator)
	{
		validator = Validator.Get<TSelf>.DoNotThrow();
		createdObject = TSelf.Create(validator);

		return validator.IsValid;
	}
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TSelf"/>
/// by providing parameter <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="TSelf">The type to be created.</typeparam>
public interface ICreatable<out TSelf, in T>
	where TSelf : ICreatable<TSelf, T>, IDomainObject
{
	static abstract TSelf Create(T parameter, Validator? validator = null);
	
	public static bool TryCreate(T parameter, [NotNullWhen(true)] out TSelf createdObject) 
		=> TryCreate(parameter, out createdObject, out _);

	public static bool TryCreate(T parameter, [NotNullWhen(true)] out TSelf createdObject, out Validator validator)
	{
		validator = Validator.Get<TSelf>.DoNotThrow();
		createdObject = TSelf.Create(parameter, validator);

		return validator.IsValid;
	}
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TSelf"/>
/// by providing parameters <typeparamref name="T1"/> and <typeparamref name="T2"/>.
/// </summary>
/// <typeparam name="TSelf">The type to be created.</typeparam>
public interface ICreatable<out TSelf, in T1, in T2>
	where TSelf : ICreatable<TSelf, T1, T2>, IDomainObject
{
	static abstract TSelf Create(T1 parameter1, T2 parameter2, Validator? validator = null);
	
	public static bool TryCreate(T1 parameter1, T2 parameter2, [NotNullWhen(true)] out TSelf createdObject) 
		=> TryCreate(parameter1, parameter2, out createdObject, out _);

	public static bool TryCreate(T1 parameter1, T2 parameter2, [NotNullWhen(true)] out TSelf createdObject, out Validator validator)
	{
		validator = Validator.Get<TSelf>.DoNotThrow();
		createdObject = TSelf.Create(parameter1, parameter2, validator);

		return validator.IsValid;
	}
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TSelf"/>
/// by providing parameters <typeparamref name="T1"/>, <typeparamref name="T2"/> and <typeparamref name="T3"/>.
/// </summary>
/// <typeparam name="TSelf">The type to be created.</typeparam>
public interface ICreatable<out TSelf, in T1, in T2, in T3>
	where TSelf : ICreatable<TSelf, T1, T2, T3>, IDomainObject
{
	static abstract TSelf Create(T1 parameter1, T2 parameter2, T3 parameter3, Validator? validator = null);
	
	public static bool TryCreate(T1 parameter1, T2 parameter2, T3 parameter3, [NotNullWhen(true)] out TSelf createdObject) 
		=> TryCreate(parameter1, parameter2, parameter3, out createdObject, out _);

	public static bool TryCreate(T1 parameter1, T2 parameter2, T3 parameter3, [NotNullWhen(true)] out TSelf createdObject, out Validator validator)
	{
		validator = Validator.Get<TSelf>.DoNotThrow();
		createdObject = TSelf.Create(parameter1, parameter2, parameter3, validator);

		return validator.IsValid;
	}
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TSelf"/>
/// by providing parameters <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/> and <typeparamref name="T4"/>.
/// </summary>
/// <typeparam name="TSelf">The type to be created.</typeparam>
public interface ICreatable<out TSelf, in T1, in T2, in T3, in T4>
	where TSelf : ICreatable<TSelf, T1, T2, T3, T4>, IDomainObject
{
	static abstract TSelf Create(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, Validator? validator = null);
	
	public static bool TryCreate(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, [NotNullWhen(true)] out TSelf createdObject) 
		=> TryCreate(parameter1, parameter2, parameter3, parameter4, out createdObject, out _);
	
	public static bool TryCreate(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, [NotNullWhen(true)] out TSelf createdObject, out Validator validator)
	{
		validator = Validator.Get<TSelf>.DoNotThrow();
		createdObject = TSelf.Create(parameter1, parameter2, parameter3, parameter4, validator);

		return validator.IsValid;
	}
}

/// <summary>
/// Has a factory to create objects of type <typeparamref name="TSelf"/>
/// by providing parameters <typeparamref name="T1"/>, <typeparamref name="T2"/>, <typeparamref name="T3"/>, <typeparamref name="T4"/> and <typeparamref name="T5"/>.
/// </summary>
/// <typeparam name="TSelf">The type to be created.</typeparam>
public interface ICreatable<out TSelf, in T1, in T2, in T3, in T4, in T5>
	where TSelf : ICreatable<TSelf, T1, T2, T3, T4, T5>, IDomainObject
{
	static abstract TSelf Create(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, Validator? validator = null);
	
	public static bool TryCreate(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, [NotNullWhen(true)] out TSelf createdObject) 
		=> TryCreate(parameter1, parameter2, parameter3, parameter4, parameter5, out createdObject, out _);
	
	public static bool TryCreate(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, [NotNullWhen(true)] out TSelf createdObject, out Validator validator)
	{
		validator = Validator.Get<TSelf>.DoNotThrow();
		createdObject = TSelf.Create(parameter1, parameter2, parameter3, parameter4, parameter5, validator);

		return validator.IsValid;
	}
}
