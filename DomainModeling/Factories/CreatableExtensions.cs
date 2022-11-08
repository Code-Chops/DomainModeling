namespace CodeChops.DomainDrivenDesign.DomainModeling.Factories;

public static class CreatableExtensions
{
	public static bool TryCreate<TObject, T1>(T1 parameter1, [NotNullWhen(true)] out TObject createdObject, ref Validator<TObject> validator)
		where TObject : ICreatable<TObject, T1>, IDomainObject
	{
		createdObject = TObject.Create(parameter1, validator);

		return validator.IsValid;
	}
	
	public static bool TryCreate<TObject, T1, T2>(T1 parameter1, T2 parameter2, [NotNullWhen(true)] out TObject createdObject, ref Validator<TObject> validator)
		where TObject : ICreatable<TObject, T1, T2>, IDomainObject
	{
		createdObject = TObject.Create(parameter1, parameter2, validator);

		return validator.IsValid;
	}
	
	public static bool TryCreate<TObject, T1, T2, T3>(T1 parameter1, T2 parameter2, T3 parameter3, [NotNullWhen(true)] out TObject createdObject, ref Validator<TObject> validator)
		where TObject : ICreatable<TObject, T1, T2, T3>, IDomainObject
	{
		createdObject = TObject.Create(parameter1, parameter2, parameter3, validator);

		return validator.IsValid;
	}
	
	public static bool TryCreate<TObject, T1, T2, T3, T4>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, [NotNullWhen(true)] out TObject createdObject, ref Validator<TObject> validator)
		where TObject : ICreatable<TObject, T1, T2, T3, T4>, IDomainObject
	{
		createdObject = TObject.Create(parameter1, parameter2, parameter3, parameter4, validator);

		return validator.IsValid;
	}
	
	public static bool TryCreate<TObject, T1, T2, T3, T4, T5>(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, [NotNullWhen(true)] out TObject? createdObject, ref Validator<TObject> validator)
		where TObject : ICreatable<TObject, T1, T2, T3, T4, T5>, IDomainObject
	{
		createdObject = TObject.Create(parameter1, parameter2, parameter3, parameter4, parameter5, validator);

		return validator.IsValid;
	}
}
