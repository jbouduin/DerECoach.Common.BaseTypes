using System;

namespace DerECoach.Common.BaseTypes
{
    /// <summary>
    /// The message level of a message
    /// </summary>
    [Flags]
    public enum EMessageLevel
    {
        None = 0,
        Debug = 1,
        Verbose = 2,
        Info = 4,
        Warning = 8,
        Error = 16,
        Fatal = 32
    }
}