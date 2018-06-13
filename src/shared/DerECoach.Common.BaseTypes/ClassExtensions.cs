using System;

namespace DerECoach.Common.BaseTypes
{
    public static class ClassExtensions
    {
        #region NotNull -------------------------------------------------------
        /// <summary>
        /// Performs the provided Action if the class is not null
        /// </summary>
        public static void IfNotNull<TObject>(this TObject value, Action<TObject> action) where TObject : class
        {
            if (value != null) action(value);
        }

        /// <summary>
        /// Performs the provided Action if the class is not null, otherwise perform the nullAction
        /// </summary>
        public static void IfNotNull<TObject>(this TObject value, Action<TObject> notNullAction, Action nullAction) where TObject : class
        {
            if (value != null)
                notNullAction(value);
            else
                nullAction();
        }

        /// <summary>
        /// Returns the result of the provided function if the class is not null.
        /// If the class is null, the default value of TResult is returned
        /// </summary>
        public static TResult IfNotNull<TObject, TResult>(this TObject value, Func<TObject, TResult> function)
            where TObject : class
        {
            return value != null ? function(value) : default(TResult);
        }

        /// <summary>
        /// Returns the result of the provided function if the class is not null.
        /// If the class is null, the passed default value returned
        /// </summary>
        public static TResult IfNotNull<TObject, TResult>(this TObject value, Func<TObject, TResult> function,
            TResult defaultValue) where TObject : class
        {
            return value != null ? function(value) : defaultValue;
        }

        /// <summary>
        /// Returns the result of the provided notNullFunction if the class is not null.
        /// If the class is null, the result of nullFunction is returned
        /// </summary>
        public static TResult IfNotNull<TObject, TResult>(this TObject value, Func<TObject, TResult> notNullFunction,
            Func<TResult> nullFunction) where TObject : class
        {
            return value != null ? notNullFunction(value) : nullFunction();
        }
        #endregion
    }
}