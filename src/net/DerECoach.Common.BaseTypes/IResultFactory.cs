namespace DerECoach.Common.BaseTypes
{
    public interface IResultFactory
    {
        #region Result --------------------------------------------------------
        /// <summary>
        /// create a success Result
        /// </summary>
        /// <returns></returns>
        IResult Success();

        /// <summary>
        /// create a success Result with message and messagelevel
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Info</param>
        /// <returns></returns>
        IResult Success(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info);

        /// <summary>
        /// create a failure Result
        /// </summary>
        /// /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IResult Failure(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Error);
        #endregion

        #region Result<TReason> -----------------------------------------------
        /// <summary>
        /// create a success Result without any parameters
        /// </summary>
        /// <returns></returns>
        IResult<TReason> Success<TReason>();

        /// <summary>
        /// create a success Result with message and messagelevel,
        /// without specifying the reason
        /// </summary>
        /// <returns></returns>
        IResult<TReason> Success<TReason>(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info);

        /// <summary>
        /// create a failure Result with message and messagelevel,
        /// without specifying the reason
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IResult<TReason> Failure<TReason>(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Error);

        /// <summary>
        /// create a failure Result with message, messageLevel and Reason
        /// </summary>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IResult<TReason> Failure<TReason>(
            TReason failureReason, 
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Error);
        #endregion

        #region ResultEx<TReason, TContext> -----------------------------------
        /// <summary>
        /// create a success Result
        /// </summary>
        /// <returns></returns>
        IResultEx<TReason, TContext> Success<TReason, TContext>();

        /// <summary>
        /// create a success Result with message and messagelevel,
        /// without specifying the reason or context
        /// </summary>
        /// <returns></returns>
        IResultEx<TReason, TContext> Success<TReason, TContext>(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info);

        /// <summary>
        /// create a failure Result with message and messagelevel,
        /// without specifying the reason or context
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IResultEx<TReason, TContext> Failure<TReason, TContext>(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Error);

        /// <summary>
        /// create a failure Result with message, messageLevel and Reason
        /// but no context
        /// </summary>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IResultEx<TReason, TContext> Failure<TReason, TContext>(
            TReason failureReason, 
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Error);

        /// <summary>
        /// create a failure Result with message, messageLevel and Reason
        /// but no context
        /// </summary>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="failureContext">the failure context</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        IResultEx<TReason, TContext> Failure<TReason, TContext>(
            TReason failureReason, 
            TContext failureContext, 
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Error);
        #endregion
    }
}