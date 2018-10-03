namespace DerECoach.Common.BaseTypes
{
    public interface IValueResultFactory
    {
        #region Result<TValue> ------------------------------------------------
        /// <summary>
        /// create a success Value Result
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <returns></returns>
        IValueResult<TValue> Success<TValue>(TValue value);

        /// <summary>
        /// create a success ValueResult with message and messagelevel
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Info</param>
        /// <returns></returns>
        IValueResult<TValue> Success<TValue>(
            TValue value, 
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info);

        /// <summary>
        /// create a failure ValueResult
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResult<TValue> Failure<TValue>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error);
        #endregion

        #region Result<TValue, TReason> ---------------------------------------
        /// <summary>
        /// create a success ValueResult without any parameters
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <returns></returns>
        IValueResult<TValue, TReason> Success<TValue, TReason>(TValue value);

        /// <summary>
        /// create a success ValueResult with message and messagelevel,
        /// without specifying the reason
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResult<TValue, TReason> Success<TValue, TReason>(
            TValue value, 
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info);

        /// <summary>
        /// create a failure ValueResult with message and messagelevel,
        /// without specifying the reason
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResult<TValue, TReason> Failure<TValue, TReason>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error);

        /// <summary>
        /// create a failure ValueResult with message, messageLevel and Reason
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResult<TValue, TReason> Failure<TValue, TReason>(
            TValue value, 
            TReason failureReason, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error);
        #endregion

        #region ResultEx<TValue, TReason, TContext> ---------------------------
        /// <summary>
        /// create a success ValueResultEx
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <returns></returns>
        IValueResultEx<TValue, TReason, TContext> Success<TValue, TReason, TContext>(
            TValue value);

        /// <summary>
        /// create a success ValueResultEx with message and messagelevel,        
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResultEx<TValue, TReason, TContext> Success<TValue, TReason, TContext>(
            TValue value, 
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info);

        /// <summary>
        /// create a failure ValueResultEx with message and messagelevel,
        /// without specifying the reason or context
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResultEx<TValue, TReason, TContext> Failure<TValue, TReason, TContext>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error);

        /// <summary>
        /// create a failure ValueResultEx with message, messageLevel and Reason
        /// but no context
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="failureReason">the reason for the failure</param>        
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResultEx<TValue, TReason, TContext> Failure<TValue, TReason, TContext>(
            TValue value, 
            TReason failureReason, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error);

        /// <summary>
        /// create a failure ValueResultEx with message, messageLevel, Reason and context        
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="failureContext">the failure context</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IValueResultEx<TValue, TReason, TContext> Failure<TValue, TReason, TContext>(
            TValue value, 
            TReason failureReason, 
            TContext failureContext, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error);
        #endregion









    }
}