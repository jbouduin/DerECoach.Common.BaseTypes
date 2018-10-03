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
    internal class ValueResultEx<TValue, TReason, TContext> : ResultEx<TReason, TContext>,
        IValueResultEx<TValue, TReason, TContext>
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
        public IValueResultEx<TValue, TReason, TContext> OnFailure(
            Action<IValueResultEx<TValue, TReason, TContext>> failureAction)
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
        public IValueResultEx<TValue, TReason, TContext> OnSuccess(
            Action<IValueResultEx<TValue, TReason, TContext>> successAction)
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
        public IValueResultEx<TValue, TReason, TContext> Continue(
            Func<IValueResultEx<TValue, TReason, TContext>, IValueResultEx<TValue, TReason, TContext>> continuefunc)
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
        public IValueResultEx<TNew, TReason, TContext> Convert<TNew>(
            Func<IValueResultEx<TValue, TReason, TContext>, TNew> conversionFunc)
        {
            var result = new ValueResultEx<TNew, TReason, TContext>
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
        public IValueResultEx<TNew, TReason, TContext> Convert<TNew>(TNew value)
        {
            var result = new ValueResultEx<TNew, TReason, TContext>
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
        public IValueResultEx<TNew, TReason, TContext> Convert<TNew>(
            Func<IValueResultEx<TValue, TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> successFunc,
            Func<IValueResultEx<TValue, TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> failureFunc = null)
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
        public IEnumerable<IValueResultEx<TNew, TReason, TContext>> Convert<TNew>(
            Func<IValueResultEx<TValue, TReason, TContext>, IEnumerable<IValueResultEx<TNew, TReason, TContext>>> successFunc,
            Func<IValueResultEx<TValue, TReason, TContext>, IEnumerable<IValueResultEx<TNew, TReason, TContext>>> failureFunc = null)
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
        public IValueResultEx<TNew, TReason, TContext> Cast<TNew>() where TNew : class
        {
            return Convert(value => this.Value as TNew);
        }
        #endregion
    }

    [DataContract]
    internal class ValueResult<TValue, TReason> : ValueResultEx<TValue, TReason, string>, IValueResult<TValue, TReason>
    {
        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IValueResult<TValue, TReason> OnFailure(
            Action<IValueResult<TValue, TReason>> failureAction)
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
        public IValueResult<TValue, TReason> OnSuccess(
            Action<IValueResult<TValue, TReason>> successAction)
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
        public IValueResult<TValue, TReason> Continue(
            Func<IValueResult<TValue, TReason>, IValueResult<TValue, TReason>> continuefunc)
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
        public IValueResult<TNew, TReason> Convert<TNew>(
            Func<IValueResult<TValue, TReason>, TNew> conversionFunc)
        {
            var result = new ValueResult<TNew, TReason>
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
        public new IValueResult<TNew, TReason> Convert<TNew>(TNew value)
        {
            var result = new ValueResult<TNew, TReason>
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
        public IValueResult<TNew, TReason> Convert<TNew>(
            Func<IValueResult<TValue, TReason>, IValueResult<TNew, TReason>> successFunc,
            Func<IValueResult<TValue, TReason>, IValueResult<TNew, TReason>> failureFunc = null)
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
        public IEnumerable<IValueResult<TNew, TReason>> Convert<TNew>(
            Func<IValueResult<TValue, TReason>, IEnumerable<IValueResult<TNew, TReason>>> successFunc,
            Func<IValueResult<TValue, TReason>, IEnumerable<IValueResult<TNew, TReason>>> failureFunc = null)
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
        public new IValueResult<TNew, TReason> Cast<TNew>() where TNew : class
        {
            return Convert(value => this.Value as TNew);
        }
        #endregion
    }

    [DataContract]
    internal class ValueResult<TValue> : ValueResultEx<TValue, int, string>, IValueResult<TValue>
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
