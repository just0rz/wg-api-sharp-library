using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WGSharpAPI.Tools
{
    public static class EnumHelper<T>
    {
        public static string GetEnumDescription(T enumValue)
        {
            var TEnumFields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            var TEnumField = TEnumFields.First(t => t.Name.Equals(enumValue.ToString()));

            var customAttribute = TEnumField.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

            if (customAttribute != null)
                return ((DescriptionAttribute)customAttribute).Description;

            return TEnumField.Name;
        }

        static EnumHelper()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("EnumHelper likes only enums");
        }
    }
}
