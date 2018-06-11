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
    public class Result<TReason, TContext, TValue> : Result<TReason, TContext>
    {
        #region datamember properties -----------------------------------------

        /// <summary>
        /// The return value of the method returning the result
        /// </summary>
        [DataMember]
        public TValue Value { get; set; }

        #endregion

        #region factory methods -----------------------------------------------

        public static Result<TReason, TContext, TValue> Success(TValue value)
        {
            return new Result<TReason, TContext, TValue> { Value = value };
        }

        public static Result<TReason, TContext, TValue> Success(TValue value, string message,
            EMessageLevel messageLevel)
        {
            return new Result<TReason, TContext, TValue>
            {
                Value = value,
                Message = message,
                MessageLevel = messageLevel
            };
        }

        public static Result<TReason, TContext, TValue> Failure(TValue value, string message)
        {
            return new Result<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false,
                Message = message,
                MessageLevel = EMessageLevel.Error
            };
        }

        public static Result<TReason, TContext, TValue> Failure(
            TValue value,
            TReason failureReason,
            string message)
        {
            return new Result<TReason, TContext, TValue>
            {
                Value = value,
                Succeeded = false,
                FailureReason = failureReason,
                Message = message,
                MessageLevel = EMessageLevel.Error
            };
        }

        public static Result<TReason, TContext, TValue> Failure(
            TValue value,
            TReason failureReason,
            TContext failureContext,
            string message)
        {
            return new Result<TReason, TContext, TValue>
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
        /// Convert to a Result with Value, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext, TNew> Return<TNew>(
            Func<Result<TReason, TContext, TValue>, TNew> conversionFunc)
        {
            return new Result<TReason, TContext, TNew>
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
        /// If Succeeded the return value of successFunc is used.
        /// If Failed and failureFunc is set, the return value of failureFunc is used,
        /// otherwise this is returned
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext> Continue(
            Func<Result<TReason, TContext, TValue>> successFunc,
            Func<Result<TReason, TContext, TValue>> failureFunc = null)
        {
            return Succeeded ? successFunc() : failureFunc == null ? this : failureFunc();
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
        public Result<TReason, TContext, TNew> Continue<TNew>(
            Func<Result<TReason, TContext, TValue>, Result<TReason, TContext, TNew>> successFunc,
            Func<Result<TReason, TContext, TValue>, Result<TReason, TContext, TNew>> failureFunc = null)
        {
            return Succeeded ? successFunc(this) : failureFunc == null ? Return(default(TNew)) : failureFunc(this);
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
        public IEnumerable<Result<TReason, TContext, TNew>> Continue<TNew>(
            Func<Result<TReason, TContext, TValue>, IEnumerable<Result<TReason, TContext, TNew>>> successFunc,
            Func<Result<TReason, TContext, TValue>, IEnumerable<Result<TReason, TContext, TNew>>> failureFunc = null)
        {
            return Succeeded
                ? successFunc(this)
                : failureFunc == null ? new[] {Return(default(TNew))} : failureFunc(this);
        }

        /// <summary>
        /// Perform the provided Action if the result is Failed
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext> OnFailure(Action<Result<TReason, TContext, TValue>> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext> OnSuccess(Action<Result<TReason, TContext, TValue>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        public Result<TReason, TContext, TNew> Cast<TNew>() where TNew : class
        {
            return Return(value => value.Value as TNew);
        }
        #endregion
    }
}
