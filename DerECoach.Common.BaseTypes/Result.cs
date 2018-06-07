using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace DerECoach.Common.BaseTypes
{
    /// <summary>
    /// Result class that can be used as return value for functions.
    /// </summary>
    /// <typeparam name="TReason">The type of Reason that will be used</typeparam>
    /// <typeparam name="TContext">The type of Context that will be used</typeparam>
    [DataContract]
    public class Result<TReason, TContext>
    {
        #region datamember properties -----------------------------------------

        [DataMember]
        public bool Succeeded { get; set; }

        [DataMember]
        public TReason FailureReason { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public EMessageLevel MessageLevel { get; set; }

        [DataMember]
        public TContext FailureContext { get; set; }

        #endregion

        #region properties ----------------------------------------------------
        public bool Failed
        {
            get { return !Succeeded; }
        }

        #endregion

        #region constructor ---------------------------------------------------

        protected Result()
        {
            Succeeded = true;
            MessageLevel = EMessageLevel.None;
            FailureReason = default(TReason);
            FailureContext = default(TContext);
        }

        #endregion

        #region factory methods -----------------------------------------------

        public static Result<TReason, TContext> Success()
        {
            return new Result<TReason, TContext>();
        }

        public static Result<TReason, TContext> Success(string message, EMessageLevel messageLevel)
        {
            return new Result<TReason, TContext>
            {
                Message = message,
                MessageLevel = messageLevel
            };
        }

        public static Result<TReason, TContext> Failure(string message)
        {
            return new Result<TReason, TContext>
            {
                Succeeded = false,
                Message = message,
                MessageLevel = EMessageLevel.Error
            };
        }

        public static Result<TReason, TContext> Failure(TReason failureReason, string message)
        {
            return new Result<TReason, TContext>
            {
                Succeeded = false,
                FailureReason = failureReason,
                Message = message,
                MessageLevel = EMessageLevel.Error
            };
        }

        public static Result<TReason, TContext> Failure(TReason failureReason,
            TContext failureContext, string message)
        {
            return new Result<TReason, TContext>
            {
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
        /// Converts to a Result with Value, setting Value to the passed value.
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext, TNew> Return<TNew>(TNew value)
        {
            return new Result<TReason, TContext, TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                Message = Message,
                MessageLevel = MessageLevel,
                FailureContext = FailureContext,
                Value = value
            };
        }

        /// <summary>
        /// Convert to a Result with Value, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext, TNew> Return<TNew>(
            Func<Result<TReason, TContext>, TNew> conversionFunc)
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
            Func<Result<TReason, TContext>> successFunc,
            Func<Result<TReason, TContext>> failureFunc = null)
        {
            return Succeeded ? successFunc() : failureFunc == null ? this : failureFunc();
        }

        /// <summary>
        /// Returns a new Result with Value.
        /// If Succeeded the Value is set to the return value of successFunc.
        /// If Failed and failureFunc is set, the Value is set to return value of failureFunc,
        /// otherwise Value is set to the default of TNew.
        /// All other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext, TNew> Continue<TNew>(
            Func<TNew> successFunc,
            Func<TNew> failureFunc = null)
        {
            return Succeeded
                ? Return(successFunc())
                : failureFunc == null ? Return(default(TNew)) : Return(failureFunc());
        }

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext, TNew> Continue<TNew>(
            Func<Result<TReason, TContext, TNew>> successFunc,
            Func<Result<TReason, TContext, TNew>> failureFunc = null)
        {
            return Succeeded ? successFunc() : failureFunc == null ? Return(default(TNew)) : failureFunc();
        }

        /// <summary>
        /// Returns an IEnumerable of Results. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise an IEnumerable containing the default Value of TNew as only itemis returned 
        /// and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IEnumerable<Result<TReason, TContext, TNew>> Continue<TNew>(
            Func<IEnumerable<Result<TReason, TContext, TNew>>> successFunc,
            Func<IEnumerable<Result<TReason, TContext, TNew>>> failureFunc)
        {
            return Succeeded ? successFunc() : failureFunc == null ? new[] {Return(default(TNew))} : failureFunc();
        }

        /// <summary>
        /// Perform the provided Action if the result is Failed
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public Result<TReason, TContext> OnFailure(Action<Result<TReason, TContext>> failureAction)
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
        public Result<TReason, TContext> OnSuccess(Action<Result<TReason, TContext>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }
        
        #endregion
    }

    
}