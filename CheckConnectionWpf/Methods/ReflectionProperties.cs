using CheckConnectionWpf.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CheckConnectionWpf.Methods
{
    static class ReflectionProperties<T>
    {
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static List<PropertyValue> GetPropertiesValueList(T refclass)
        {
            var properties = typeof(T).GetProperties()
                               .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
                               .Select(p => new
                               {
                                   PropertyName = p.Name,
                                   p.GetCustomAttributes(typeof(DisplayAttribute),
                                  false).Cast<DisplayAttribute>().Single().Name
                               });

            List<PropertyValue> propertyValueList = new List<PropertyValue>();

            foreach (var propinfo in properties)
            {
                PropertyValue propertyValue = new PropertyValue();
                try
                {
                    propertyValue.Value = GetPropValue(refclass, propinfo.PropertyName)?.ToString();
                    propertyValue.Name = propinfo.Name?.ToString();                    
                }
                catch (Exception ex)
                {
                }
                propertyValueList.Add(propertyValue);
            }
            return propertyValueList;
        }
        public static List<string> GetPropertiesNameList(T refclass)
        {
            var properties = typeof(T).GetProperties()
                               .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
                               .Select(p => new
                               {
                                   PropertyName = p.Name,
                                   p.GetCustomAttributes(typeof(DisplayAttribute),
                                  false).Cast<DisplayAttribute>().Single().Name
                               });

            List<string> propertyNameList = new List<string>();

            string value = string.Empty;
            foreach (var propinfo in properties)
            {
                value = string.Empty;
                try
                {
                    value = propinfo.PropertyName.ToString();
                }
                catch (Exception ex)
                {
                }
                propertyNameList.Add(value);
            }
            return propertyNameList;
        }
    }
}
