using System;

namespace DerECoach.Common.BaseTypes
{
    public static class NullableExtensions
    {
        #region NotNullOrDefault ----------------------------------------------
        /// <summary>
        /// Performs the provided Action if the nullable class is not null and is not the default value
        /// </summary>
        public static void IfNotNullOrDefault<TObject>(this TObject? value, Action<TObject> action) where TObject : struct
        {
            if (value != null && !value.Value.Equals(default(TObject))) action(value.Value);
        }

        /// <summary>
        /// Performs the provided Action if the nullable class is not null and is not the default value, otherwise perform the nullAction
        /// </summary>
        public static void IfNotNullOrDefault<TObject>(this TObject? value, Action<TObject> notNullAction, Action nullAction) where TObject : struct
        {
            if (value != null && !value.Value.Equals(default(TObject)))
                notNullAction(value.Value);
            else
                nullAction();
        }

        /// <summary>
        /// Returns the result of the provided function if the class is not null and is not the default value.
        /// If the class is null, the default value of TResult is returned
        /// </summary>
        public static TResult IfNotNullOrDefault<TObject, TResult>(this TObject? value, Func<TObject, TResult> function) where TObject : struct
        {
            return value != null && !value.Value.Equals(default(TObject)) ? function(value.Value) : default(TResult);
        }

        /// <summary>
        /// Returns the result of the provided function if the class is not null.
        /// If the class is null or the default, the passed default value returned
        /// </summary>
        public static TResult IfNotNullOrDefault<TObject, TResult>(this TObject? value, Func<TObject, TResult> function, TResult defaultValue) where TObject : struct
        {
            return value != null && !value.Value.Equals(default(TObject)) ? function(value.Value) : defaultValue;
        }

        /// <summary>
        /// Returns the result of the provided notNullFunction if the class is not null and is not the default value.
        /// If the class is null, the result of the provided nullFunction is executed
        /// </summary>
        public static TResult IfNotNullOrDefault<TObject, TResult>(this TObject? value, Func<TObject, TResult> notNullfunction, Func<TResult> nullfunction) where TObject : struct
        {
            return value != null && !value.Value.Equals(default(TObject)) ? notNullfunction(value.Value) : nullfunction();
        }
        #endregion
    }
}