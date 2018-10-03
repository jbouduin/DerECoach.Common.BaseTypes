namespace DerECoach.Common.BaseTypes
{
    public class ResultFactoryProvider
    {
        public static IResultFactory GetResultFactory()
        {
            return new ResultFactory();
        }
    }

    internal class ResultFactory : IResultFactory
    {
        #region Result --------------------------------------------------------
        /// <summary>
        /// create a success Result
        /// </summary>
        /// <returns></returns>
        public IResult Success()
        {
            return new Result();
        }

        /// <summary>
        /// create a success Result with message and messagelevel
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Info</param>
        /// <returns></returns>
        public IResult Success(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info)
        {
            var result = new Result();
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure Result
        /// </summary>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IResult Failure(
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new Result
            {
                Succeeded = false
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        #endregion

        #region Result<TReason> -----------------------------------------------
        /// <summary>
        /// create a success Result without any parameters
        /// </summary>        
        /// <returns></returns>
        public IResult<TReason> Success<TReason>()
        {
            return new Result<TReason>();
        }

        /// <summary>
        /// create a success Result with message and messagelevel        
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Info</param>
        /// <returns></returns>
        public IResult<TReason> Success<TReason>(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info)
        {
            var result = new Result<TReason>();
            result.SetMessage(message, messageLevel);
            return result;            
        }

        /// <summary>
        /// create a failure Result with message and messagelevel,
        /// without specifying the reason
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IResult<TReason> Failure<TReason>(
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new Result<TReason>
            {
                Succeeded = false
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure Result with message, messageLevel and Reason
        /// </summary>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IResult<TReason> Failure<TReason>(
            TReason failureReason, 
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new Result<TReason>
            {
                Succeeded = false,
                FailureReason = failureReason
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        #endregion

        #region ResultEx<TReason, TContext> -----------------------------------
        /// <summary>
        /// create a success Result
        /// </summary>
        /// <returns></returns>
        public IResultEx<TReason, TContext> Success<TReason, TContext>() 
        {
            return new ResultEx<TReason, TContext>();
        }

        /// <summary>
        /// create a success Result with message and messagelevel,
        /// without specifying the reason or context
        /// </summary>
        /// <returns></returns>
        public IResultEx<TReason, TContext> Success<TReason, TContext>(
            string message, 
            EMessageLevel messageLevel = EMessageLevel.Info)
        {
            var result = new ResultEx<TReason, TContext>();
            result.SetMessage(message, messageLevel);
            return result;            
        }

        /// <summary>
        /// create a failure Result with message and messagelevel,
        /// without specifying the reason or context
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IResultEx<TReason, TContext> Failure<TReason, TContext>(
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ResultEx<TReason, TContext>
            {
                Succeeded = false
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure Result with message, messageLevel and Reason
        /// but no context
        /// </summary>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IResultEx<TReason, TContext> Failure<TReason, TContext>(
            TReason failureReason,
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ResultEx<TReason, TContext>
            {
                Succeeded = false,
                FailureReason = failureReason
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure Result with message, messageLevel and Reason
        /// but no context
        /// </summary>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="failureContext">the failure context</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IResultEx<TReason, TContext> Failure<TReason, TContext>(
            TReason failureReason,
            TContext failureContext,
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ResultEx<TReason, TContext>
            {
                Succeeded = false,
                FailureReason = failureReason,
                FailureContext = failureContext
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        #endregion

        

        
    }
}
