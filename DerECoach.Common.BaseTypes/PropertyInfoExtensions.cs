using System.Reflection;

namespace DerECoach.Common.BaseTypes
{
    public static class PropertyInfoExtensions
    {
        public static object GetValue(this PropertyInfo propertyInfo, object obj)
        {
            return propertyInfo.GetValue(obj, null);
        }

        public static void SetValue(this PropertyInfo propertyInfo, object obj, object value)
        {
            if (propertyInfo.GetSetMethod() != null) propertyInfo.SetValue(obj, value, null);
        }

        public static bool HasDefaultValue(this PropertyInfo propertyInfo, object obj)
        {
            var value = propertyInfo.GetValue(obj);
            return value == null || value.Equals(propertyInfo.PropertyType.GetDefault());
        }
    }
}