﻿namespace CodeChops.DomainDrivenDesign.DomainModeling.Extensions;

public static class DomainObjectExtensions
{
	public static string ToEasyString(this IDomainObject domainObject, object? parameters = null, string? extraText = null)
		=> EasyStringHelper.ToEasyString(domainObject.GetType(), parameters, extraText); 
}