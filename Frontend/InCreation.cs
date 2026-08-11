using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Frontend
{
    static class InCreation
    {
        public static T Creation<T>(Dictionary<string, Func<object>> dictionary, List<PropertyInfo> properties = null) where T : new()
        {
            T dto = new T();
            if (properties == null)
            {
                properties = typeof(T).GetProperties().ToList();
            }


            foreach (var property in properties)
            {
                if (dictionary.TryGetValue(property.Name, out var propFunc))
                {
                    var value = propFunc();
                    property.SetValue(dto, value);
                }
            }
            return dto;
        }
    }
}
