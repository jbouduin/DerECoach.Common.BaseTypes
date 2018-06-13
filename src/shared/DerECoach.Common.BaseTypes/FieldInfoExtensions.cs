using System.Reflection;

namespace DerECoach.Common.BaseTypes
{
    public static class FieldInfoExtensions
    {
        public static object GetValue(this FieldInfo fieldInfo, object obj)
        {
            return fieldInfo.GetValue(obj);
        }

        public static void SetValue(this FieldInfo fieldInfo, object obj, object value)
        {
            fieldInfo.SetValue(obj, value);
        }

        public static bool HasDefaultValue(this FieldInfo fieldInfo, object obj)
        {
            var value = fieldInfo.GetValue(obj);
            return value == null || value.Equals(fieldInfo.FieldType.GetDefault());
        }
    }
}