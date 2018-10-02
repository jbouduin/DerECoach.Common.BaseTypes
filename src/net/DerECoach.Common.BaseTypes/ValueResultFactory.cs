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
        #region ValueResultEx<TReason, TContext, TValue> ----------------------

        public IValueResultEx<TReason, TContext, TValue> Success<TReason, TContext, TValue>(TValue value)
        {
            return new ValueResultEx<TReason, TContext, TValue> { Value = value };
        }

        public IValueResultEx<TReason, TContext, TValue> Success<TReason, TContext, TValue>(TValue value, string message,
            EMessageLevel messageLevel)
        {
            var result = new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        public IValueResultEx<TReason, TContext, TValue> Failure<TReason, TContext, TValue>(TValue value, string message)
        {
            var result = new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false
            };
            result.SetMessage(message, EMessageLevel.Error);
            return result;
        }

        public IValueResultEx<TReason, TContext, TValue> Failure<TReason, TContext, TValue>(
            TValue value,
            TReason failureReason,
            string message)
        {
            var result = new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason
            };
            result.SetMessage(message, EMessageLevel.Error);
            return result;
        }

        public IValueResultEx<TReason, TContext, TValue> Failure<TReason, TContext, TValue>(
            TValue value,
            TReason failureReason,
            TContext failureContext,
            string message)
        {
            var result = new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason,
                FailureContext = failureContext
            };
            result.SetMessage(message, EMessageLevel.Error);
            return result;
        }

        #endregion

        #region ValueResultEx<TReason, TValue> --------------------------------

        public IValueResult<TReason, TValue> Success<TReason, TValue>(TValue value)
        {
            return new ValueResult<TReason, TValue> { Value = value };
        }

        public IValueResult<TReason, TValue> Success<TReason, TValue>(TValue value, string message,
            EMessageLevel messageLevel)
        {
            var result = new ValueResult<TReason, TValue>
            {
                Value = value
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        public IValueResult<TReason, TValue> Failure<TReason, TValue>(TValue value, string message)
        {
            var result = new ValueResult<TReason, TValue>
            {
                Value = value,
                Succeeded = false
            };
            result.SetMessage(message, EMessageLevel.Error);
            return result;
        }

        public IValueResult<TReason, TValue> Failure<TReason, TValue>(
            TValue value,
            TReason failureReason,
            string message)
        {
            var result = new ValueResult<TReason, TValue>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason
            };
            result.SetMessage(message, EMessageLevel.Error);
            return result;
        }
                
        #endregion

        #region ValueResultLtdEx<TValue> --------------------------------------

        public IValueResult<TValue> Success<TValue>(TValue value)
        {
            return new ValueResult<TValue> { Value = value };
        }

        public IValueResult<TValue> Success<TValue>(TValue value, string message,
            EMessageLevel messageLevel)
        {
            var result = new ValueResult<TValue>
            {
                Value = value
            };
            result.SetMessage(message, messageLevel);
            return result;
        }

        public IValueResult<TValue> Failure<TValue>(TValue value, string message)
        {
            var result = new ValueResult<TValue>
            {
                Value = value,
                Succeeded = false
            };
            result.SetMessage(message, EMessageLevel.Error);
            return result;
        }  

        

        #endregion
    }
}
