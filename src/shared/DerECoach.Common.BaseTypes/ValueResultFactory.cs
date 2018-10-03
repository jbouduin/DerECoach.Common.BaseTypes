namespace DerECoach.Common.BaseTypes
{
    public class ValueResultFactoryProvider
    {
        public static IValueResultFactory GetValueResultFactory()
        {
            return new ValueResultFactory();
        }
    }

    internal class ValueResultFactory : IValueResultFactory
    {
        #region ValueResult<TValue> -------------------------------------------
        /// <summary>
        /// create a success Value Result
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <returns></returns>
        public IValueResult<TValue> Success<TValue>(TValue value)
        {
            return new ValueResult<TValue> { Value = value };
        }

        /// <summary>
        /// create a success ValueResult with message and messagelevel
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Info</param>
        /// <returns></returns>
        public IValueResult<TValue> Success<TValue>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Info)
        {
            var result = new ValueResult<TValue>
            {
                Value = value
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure ValueResult
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResult<TValue> Failure<TValue>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ValueResult<TValue>
            {
                Value = value,
                Succeeded = false
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        #endregion

        #region ValueResult<TValue, TReason> ----------------------------------
        /// <summary>
        /// create a success ValueResult without any parameters
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <returns></returns>
        public IValueResult<TValue, TReason> Success<TValue, TReason>(TValue value)
        {
            return new ValueResult<TValue, TReason> { Value = value };
        }
        /// <summary>
        /// create a success ValueResult with message and messagelevel,
        /// without specifying the reason
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResult<TValue, TReason> Success<TValue, TReason>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Info)
        {
            var result = new ValueResult<TValue, TReason>
            {
                Value = value
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure ValueResult with message and messagelevel,
        /// without specifying the reason
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResult<TValue, TReason> Failure<TValue, TReason>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ValueResult<TValue, TReason>
            {
                Value = value,
                Succeeded = false
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure ValueResult with message, messageLevel and Reason
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResult<TValue, TReason> Failure<TValue, TReason>(
            TValue value,
            TReason failureReason,
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ValueResult<TValue, TReason>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        #endregion

        #region ValueResultEx<TValue, TReason, TContext> ----------------------
        /// <summary>
        /// create a success ValueResult
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <returns></returns>
        public IValueResultEx<TValue, TReason, TContext> Success<TValue, TReason, TContext>(TValue value)
        {
            return new ValueResultEx<TValue, TReason, TContext> { Value = value };
        }

        /// <summary>
        /// create a success ValueResultEx with message and messagelevel,
        /// without specifying the reason or context
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResultEx<TValue, TReason, TContext> Success<TValue, TReason, TContext>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Info)
        {
            var result = new ValueResultEx<TValue, TReason, TContext>
            {
                Value = value
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure ValueResultEx with message and messagelevel,
        /// without specifying the reason or context
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="message">the message</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResultEx<TValue, TReason, TContext> Failure<TValue, TReason, TContext>(
            TValue value, 
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ValueResultEx<TValue, TReason, TContext>
            {
                Value = value,
                Succeeded = false
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure ValueResultEx with message, messageLevel and Reason
        /// but no context
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="failureReason">the reason for the failure</param>        
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResultEx<TValue, TReason, TContext> Failure<TValue, TReason, TContext>(
            TValue value,
            TReason failureReason,
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ValueResultEx<TValue, TReason, TContext>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        /// <summary>
        /// create a failure ValueResultEx with message, messageLevel, Reason and context        
        /// </summary>
        /// <param name="value">the value to be returned</param>
        /// <param name="failureReason">the reason for the failure</param>
        /// <param name="failureContext">the failure context</param>
        /// <param name="message">the message, may not be null</param>
        /// <param name="messageLevel">the message level, defaults to Error, may not be None</param>
        /// <returns></returns>
        public IValueResultEx<TValue, TReason, TContext> Failure<TValue, TReason, TContext>(
            TValue value,
            TReason failureReason,
            TContext failureContext,
            string message,
            EMessageLevel messageLevel = EMessageLevel.Error)
        {
            var result = new ValueResultEx<TValue, TReason, TContext>
            {
                Value = value,
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
