namespace DerECoach.Common.BaseTypes
{
    public static class ObjectExtensions
    {
        #region other ---------------------------------------------------------
        /// <summary>
        /// Equals-Methode which can handle when this == null
        /// </summary>
        public static bool EqualsConsideringNull<TObject>(this TObject value, TObject other)
        {
            // ReSharper disable CompareNonConstrainedGenericWithNull
            return value == null ? other == null : value.Equals(other);
            // ReSharper restore CompareNonConstrainedGenericWithNull
        }
        #endregion
    }
}