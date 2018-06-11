using System.Globalization;

namespace DerECoach.Common.BaseTypes
{
    public static class IntExtensions
    {
        public static string ToInvariantString(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}