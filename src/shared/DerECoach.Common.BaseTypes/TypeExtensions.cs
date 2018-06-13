using System;
using System.IO;

namespace DerECoach.Common.BaseTypes
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static string ReadEmbeddedResourceFile(this Type type, string filePath)
        {
            using (var stream = type.Assembly.GetManifestResourceStream(filePath))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        } 
    }
}