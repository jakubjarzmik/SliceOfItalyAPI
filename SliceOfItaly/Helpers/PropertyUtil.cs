﻿using System.Reflection;

namespace SliceOfItalyAPI.Helpers;

public static class PropertyUtil
{
    public static T CopyProperties<T, T2>(this T targetObject, T2 sourceObject) where T : class
    {
        if (targetObject == null || sourceObject == null)
        {
            return targetObject;
        }

        foreach (var property in typeof(T).GetProperties().Where(p => p.CanWrite))
        {
            Func<PropertyInfo, bool> CheckIfPropertyExistInSource =
                prop => string.Equals(property.Name, prop.Name, StringComparison.InvariantCultureIgnoreCase)
                        && prop.PropertyType.Equals(property.PropertyType);

            if (sourceObject.GetType().GetProperties().Any(CheckIfPropertyExistInSource))
            {
                property.SetValue(targetObject, sourceObject.GetPropertyValue(property.Name), null);
            }
        }
        return targetObject;
    }
    private static object GetPropertyValue<T>(this T source, string propertyName)
    {
        return source.GetType().GetProperty(propertyName).GetValue(source, null);
    }
}