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
    internal class ValueResultEx<TReason, TContext, TValue> : ResultEx<TReason, TContext>,
        IValueResultEx<TReason, TContext, TValue>
    {
        #region datamember properties -----------------------------------------

        /// <summary>
        /// The return value of the method returning the result
        /// </summary>
        [DataMember]
        public TValue Value { get; set; }

        #endregion

        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IValueResultEx<TReason, TContext, TValue> OnFailure(
            Action<IValueResultEx<TReason, TContext, TValue>> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IValueResultEx<TReason, TContext, TValue> OnSuccess(
            Action<IValueResultEx<TReason, TContext, TValue>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResultEx<TReason, TContext, TValue> Continue(
            Func<IValueResultEx<TReason, TContext, TValue>, IValueResultEx<TReason, TContext, TValue>> continuefunc)
        {
            return continuefunc(this);
        }

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResultEx<TReason, TContext, TNew> Convert<TNew>(
            Func<IValueResultEx<TReason, TContext, TValue>, TNew> conversionFunc)
        {
            var result = new ValueResultEx<TReason, TContext, TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,                
                FailureContext = FailureContext,
                Value = conversionFunc(this)
            };
            result.SetMessage(Message, MessageLevel);
            return result;
        }

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResultEx<TReason, TContext, TNew> Convert<TNew>(TNew value)
        {
            var result = new ValueResultEx<TReason, TContext, TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                FailureContext = FailureContext,
                Value = value
            };
            result.SetMessage(Message, MessageLevel);
            return result;
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
        public IValueResultEx<TReason, TContext, TNew> Convert<TNew>(
            Func<IValueResultEx<TReason, TContext, TValue>, IValueResultEx<TReason, TContext, TNew>> successFunc,
            Func<IValueResultEx<TReason, TContext, TValue>, IValueResultEx<TReason, TContext, TNew>> failureFunc = null)
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
        public IEnumerable<IValueResultEx<TReason, TContext, TNew>> Convert<TNew>(
            Func<IValueResultEx<TReason, TContext, TValue>, IEnumerable<IValueResultEx<TReason, TContext, TNew>>> successFunc,
            Func<IValueResultEx<TReason, TContext, TValue>, IEnumerable<IValueResultEx<TReason, TContext, TNew>>> failureFunc = null)
        {
            return Succeeded
                ? successFunc(this)
                : failureFunc == null ? new[] { Convert(default(TNew)) } : failureFunc(this);
        }



        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        public IValueResultEx<TReason, TContext, TNew> Cast<TNew>() where TNew : class
        {
            return Convert(value => this.Value as TNew);
        }
        #endregion
    }

    [DataContract]
    internal class ValueResult<TReason, TValue> : ValueResultEx<TReason, string, TValue>, IValueResult<TReason, TValue>
    {
        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IValueResult<TReason, TValue> OnFailure(
            Action<IValueResult<TReason, TValue>> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IValueResult<TReason, TValue> OnSuccess(
            Action<IValueResult<TReason, TValue>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResult<TReason, TValue> Continue(
            Func<IValueResult<TReason, TValue>, IValueResult<TReason, TValue>> continuefunc)
        {
            return continuefunc(this);
        }

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResult<TReason, TNew> Convert<TNew>(
            Func<IValueResult<TReason, TValue>, TNew> conversionFunc)
        {
            var result = new ValueResult<TReason, TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,                
                FailureContext = FailureContext,
                Value = conversionFunc(this)
            };
            return result;
        }

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public new IValueResult<TReason, TNew> Convert<TNew>(TNew value)
        {
            var result = new ValueResult<TReason, TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                FailureContext = FailureContext,
                Value = value
            };
            result.SetMessage(Message, MessageLevel);
            return result;
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
        public IValueResult<TReason, TNew> Convert<TNew>(
            Func<IValueResult<TReason, TValue>, IValueResult<TReason, TNew>> successFunc,
            Func<IValueResult<TReason, TValue>, IValueResult<TReason, TNew>> failureFunc = null)
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
        public IEnumerable<IValueResult<TReason, TNew>> Convert<TNew>(
            Func<IValueResult<TReason, TValue>, IEnumerable<IValueResult<TReason, TNew>>> successFunc,
            Func<IValueResult<TReason, TValue>, IEnumerable<IValueResult<TReason, TNew>>> failureFunc = null)
        {
            return Succeeded
                ? successFunc(this)
                : failureFunc == null ? new[] { Convert(default(TNew)) } : failureFunc(this);
        }



        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        public new IValueResult<TReason, TNew> Cast<TNew>() where TNew : class
        {
            return Convert(value => this.Value as TNew);
        }
        #endregion
    }

    [DataContract]
    internal class ValueResult<TValue> : ValueResultEx<int, string, TValue>, IValueResult<TValue>
    {
        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IValueResult<TValue> OnFailure(
            Action<IValueResult<TValue>> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IValueResult<TValue> OnSuccess(
            Action<IValueResult<TValue>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResult<TValue> Continue(
            Func<IValueResult<TValue>, IValueResult<TValue>> continuefunc)
        {
            return continuefunc(this);
        }

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResult<TNew> Convert<TNew>(
            Func<IValueResult<TValue>, TNew> conversionFunc)
        {
            var result = new ValueResult<TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                FailureContext = FailureContext,
                Value = conversionFunc(this)
            };
            result.SetMessage(Message, MessageLevel);
            return result;
        }

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public new IValueResult<TNew> Convert<TNew>(TNew value)
        {
            var result = new ValueResult<TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                FailureContext = FailureContext,
                Value = value
            };
            result.SetMessage(Message, MessageLevel);
            return result;
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
        public IValueResult<TNew> Convert<TNew>(
            Func<IValueResult<TValue>, IValueResult<TNew>> successFunc,
            Func<IValueResult<TValue>, IValueResult<TNew>> failureFunc = null)
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
        public IEnumerable<IValueResult<TNew>> Convert<TNew>(
            Func<IValueResult<TValue>, IEnumerable<IValueResult<TNew>>> successFunc,
            Func<IValueResult<TValue>, IEnumerable<IValueResult<TNew>>> failureFunc = null)
        {
            return Succeeded
                ? successFunc(this)
                : failureFunc == null ? new[] { Convert(default(TNew)) } : failureFunc(this);
        }



        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        public new IValueResult<TNew> Cast<TNew>() where TNew : class
        {
            return Convert(value => this.Value as TNew);
        }
        #endregion
    }

}
