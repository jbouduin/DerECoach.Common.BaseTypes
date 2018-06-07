using System;

namespace DerECoach.Common.BaseTypes
{
    public static class StringExtensions
    {
        #region NullOrEmpty ---------------------------------------------------
        /// <summary>
        /// check if the string is null or empty
        /// </summary>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Performs the provided Action if the string is not null or empty
        /// </summary>
        public static void IfNotNullOrEmpty(this string value, Action<string> action)
        {
            if (!value.IsNullOrEmpty()) action(value);
        }

        /// <summary>
        /// Performs the provided Action if the string is not null or empty, otherwise perform the nullAction
        /// </summary>
        public static void IfNotNullOrEmpty(this string value, Action<string> notNullAction, Action nullAction)
        {
            if (!value.IsNullOrEmpty()) 
                notNullAction(value);
            else 
                nullAction();
        }

        /// <summary>
        /// Executes the provided function if the string is not null or empty
        /// If the class is null, the default value of TResult is returned
        /// </summary>
        public static TResult IfNotNullOrEmpty<TResult>(this string value, Func<string, TResult> function)
        {
            return !value.IsNullOrEmpty() ? function(value) : default(TResult);
        }
        
        /// <summary>
        /// Executes the provided function if the the string is not null or empty
        /// </summary>
        public static TResult IfNotNullOrEmpty<TResult>(this string value, Func<string, TResult> function, TResult defaultValue)
        {
            return !value.IsNullOrEmpty() ? function(value) : defaultValue;
        }

        /// <summary>
        /// Executes the provided notNullFunction if the class is not null if the string is not null or empty
        /// If the class is null, the nullFunction is executed
        /// </summary>
        public static TResult IfNotNullOrEmpty<TResult>(this string value, Func<string, TResult> notNullfunction, Func<TResult> nullfunction)
        {
            return !value.IsNullOrEmpty() ? notNullfunction(value) : nullfunction();
        }
        #endregion
        
        #region NullOrWhitespace ----------------------------------------------
        /// <summary>
        /// check if the string is null or Whitespace
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Performs the provided Action if the string is not null or Whitespace
        /// </summary>
        public static void IfNotNullOrWhitespace(this string value, Action<string> action)
        {
            if (!value.IsNullOrWhiteSpace()) action(value);
        }

        /// <summary>
        /// Performs the provided Action if the string is not null or Whitespace, otherwise perform the nullAction
        /// </summary>
        public static void IfNotNullOrWhitespace(this string value, Action<string> notNullAction, Action nullAction)
        {
            if (!value.IsNullOrWhiteSpace())
                notNullAction(value);
            else
                nullAction();
        }

        /// <summary>
        /// Executes the provided function if the string is not null or Whitespace
        /// If the class is null, the default value of TResult is returned
        /// </summary>
        public static TResult IfNotNullOrWhitespace<TResult>(this string value, Func<string, TResult> function)
        {
            return !value.IsNullOrWhiteSpace() ? function(value) : default(TResult);
        }

        /// <summary>
        /// Executes the provided function if the the string is not null or Whitespace
        /// </summary>
        public static TResult IfNotNullOrWhitespace<TResult>(this string value, Func<string, TResult> function, TResult defaultValue)
        {
            return !value.IsNullOrWhiteSpace() ? function(value) : defaultValue;
        }

        /// <summary>
        /// Executes the provided notNullFunction if the class is not null if the string is not null or Whitespace
        /// If the class is null, the nullFunction is executed
        /// </summary>
        public static TResult IfNotNullOrWhitespace<TResult>(this string value, Func<string, TResult> notNullfunction, Func<TResult> nullfunction)
        {
            return !value.IsNullOrWhiteSpace() ? notNullfunction(value) : nullfunction();
        }
        #endregion

        #region Enum ----------------------------------------------------------
        /// <summary>
        /// parse the string to an enum value
        /// </summary>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        /// <summary>
        /// parse the string to a nullable enum value
        /// </summary>
        public static TEnum? ToNullableEnum<TEnum>(this string value) where TEnum : struct
        {
            return value.IsNullOrEmpty() ? (TEnum?)null : (TEnum)Enum.Parse(typeof(TEnum), value);
        }
        #endregion

        #region Other ---------------------------------------------------------
        /// <summary>
        /// Equals-Methode, die auch damit umgehen kann, wenn this == null ist
        /// </summary>
        public static bool EqualsConsideringNull(this string value, string other, StringComparison stringComparison)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            return value == null ? other == null : value.Equals(other, stringComparison);
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }

        public static bool EqualsEmptyEqualsNull(this string value, string other, StringComparison stringComparison)
        {
            return string.IsNullOrEmpty(value)
                ? string.IsNullOrEmpty(other)
                : value.Equals(other, stringComparison);
        }
        #endregion
    }
}