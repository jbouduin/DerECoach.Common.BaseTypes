using System;

namespace DerECoach.Common.BaseTypes
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Perform the provided action on all values of the enum
        /// </summary>
        public static void ForAllValues<TEnum>(Action<TEnum> action) where TEnum : struct
        {
            Enum.GetValues(typeof(TEnum)).ForEachObject(value => action((TEnum)value));
        }
    }
}