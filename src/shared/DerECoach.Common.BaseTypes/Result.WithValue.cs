using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace DerECoach.Common.BaseTypes
{


    /// <summary>
    /// Result class that can be used as return value for methods containing the result. The class is serializable.
    /// A Failure Result always has a Message
    /// </summary>
    /// <typeparam name="TReason">The type of Reason that will be used</typeparam>
    /// <typeparam name="TContext">The type of Context that will be used</typeparam>
    /// <typeparam name="TValue">The actual method result to be returned from the method</typeparam>
    [DataContract]
    internal class ValueResultEx<TReason, TContext, TValue> : ResultEx<TReason, TContext>, IValueResultEx<TReason, TContext, TValue>
    {
        #region datamember properties -----------------------------------------

        /// <summary>
        /// The return value of the method returning the result
        /// </summary>
        [DataMember]
        public TValue Value { get; set; }

        #endregion

        #region factory methods -----------------------------------------------

        public static ValueResultEx<TReason, TContext, TValue> Success(TValue value)
        {
            return new ValueResultEx<TReason, TContext, TValue> { Value = value };
        }

        public static ValueResultEx<TReason, TContext, TValue> Success(TValue value, string message,
            EMessageLevel messageLevel)
        {
            return new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value,
                Message = message,
                MessageLevel = messageLevel
            };
        }

        public static ValueResultEx<TReason, TContext, TValue> Failure(TValue value, string message)
        {
            return new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false,
                Message = message,
                MessageLevel = EMessageLevel.Error
            };
        }

        public static ValueResultEx<TReason, TContext, TValue> Failure(
            TValue value,
            TReason failureReason,
            string message)
        {
            return new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason,
                Message = message,
                MessageLevel = EMessageLevel.Error
            };
        }

        public static ValueResultEx<TReason, TContext, TValue> Failure(
            TValue value,
            TReason failureReason,
            TContext failureContext,
            string message)
        {
            return new ValueResultEx<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason,
                FailureContext = failureContext,
                Message = message,
                MessageLevel = EMessageLevel.Error
            };
        }

        #endregion

        #region fluent methods ------------------------------------------------
        

        /// <summary>
        /// Returns a new Result of the same type 
        /// If Succeeded the return value of successFunc is used.
        /// If Failed and failureFunc is set, the return value of failureFunc is used,
        /// otherwise this is returned
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public ValueResultEx<TReason, TContext, TValue> Continue(
            Func<ValueResultEx<TReason, TContext, TValue>> successFunc,
            Func<ValueResultEx<TReason, TContext, TValue>> failureFunc = null)
        {
            return Succeeded ? successFunc() : failureFunc == null ? this : failureFunc();
        }

        /// <summary>
        /// Convert to a Result with Value, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public ValueResultEx<TReason, TContext, TNew> Convert<TNew>(
            Func<ValueResultEx<TReason, TContext, TValue>, TNew> conversionFunc)
        {
            return new ValueResultEx<TReason, TContext, TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                Message = Message,
                MessageLevel = MessageLevel,
                FailureContext = FailureContext,
                Value = conversionFunc(this)
            };
        }

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserverd
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public ValueResultEx<TReason, TContext, TNew> Convert<TNew>(
            Func<ValueResultEx<TReason, TContext, TValue>, ValueResultEx<TReason, TContext, TNew>> successFunc,
            Func<ValueResultEx<TReason, TContext, TValue>, ValueResultEx<TReason, TContext, TNew>> failureFunc = null)
        {
            return Succeeded ? successFunc(this) : failureFunc == null ? Convert(default(TNew)) : failureFunc(this);
        }

        /// <summary>
        /// Returns an IEnumerable of Results. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise an IEnumerable containing the default Value of TNew as only itemis returned 
        /// and all other properties are preserverd
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IEnumerable<ValueResultEx<TReason, TContext, TNew>> Convert<TNew>(
            Func<ValueResultEx<TReason, TContext, TValue>, IEnumerable<ValueResultEx<TReason, TContext, TNew>>> successFunc,
            Func<ValueResultEx<TReason, TContext, TValue>, IEnumerable<ValueResultEx<TReason, TContext, TNew>>> failureFunc = null)
        {
            return Succeeded
                ? successFunc(this)
                : failureFunc == null ? new[] {Convert(default(TNew))} : failureFunc(this);
        }

        /// <summary>
        /// Perform the provided Action if the result is Failed
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public ValueResultEx<TReason, TContext, TValue> OnFailure(Action<ValueResultEx<TReason, TContext, TValue>> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public ValueResultEx<TReason, TContext, TValue> OnSuccess(Action<ValueResultEx<TReason, TContext, TValue>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        public ValueResultEx<TReason, TContext, TNew> Cast<TNew>() where TNew : class
        {
            return Convert(value => value.Value as TNew);
        }
        #endregion
    }

    [DataContract]
    internal class ValueResult<TReason, TValue> : ValueResultEx<TReason, string, TValue>
    {

    }

    [DataContract]
    internal class ValueResultLtd<TValue> : ValueResultEx<int, string, TValue>
    {

    }

}
