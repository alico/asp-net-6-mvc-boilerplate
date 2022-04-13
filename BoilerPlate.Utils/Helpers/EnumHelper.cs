using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace BoilerPlate.Utils
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
        }


        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.Contains(description, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.Contains(description, StringComparison.InvariantCultureIgnoreCase))
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }

        public static List<T> GetEnumList<T>()
        {
            var enums = ((T[])Enum.GetValues(typeof(T))).Where(x => !x.Equals(default(T)));
            return enums.ToList();
        }
    }
}
