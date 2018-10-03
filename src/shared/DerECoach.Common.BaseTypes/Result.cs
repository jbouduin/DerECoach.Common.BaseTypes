using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace DerECoach.Common.BaseTypes
{
    /// <summary>
    /// Result class that can be used as return value for methods instead of creating void methods. The class is serializable.
    /// A Failure Result always has a Message
    /// </summary>
    /// <typeparam name="TReason">The type of Reason that will be used</typeparam>
    /// <typeparam name="TContext">The type of Context that will be used</typeparam>
    [DataContract]
    internal class ResultEx<TReason, TContext> : IResultEx<TReason, TContext>
    {
        #region datamember properties -----------------------------------------        
        /// <summary>
        /// A boolean flag indicating that the result is a success
        /// </summary>
        [DataMember]
        public bool Succeeded { get; set; }


        /// <summary>
        /// The result message. If the result is a failure, this message is mandatory.
        /// </summary>
        [DataMember]
        public string Message { get; private set; }

        /// <summary>
        /// The Error level message
        /// </summary>
        [DataMember]
        public EMessageLevel MessageLevel { get; private set; } = EMessageLevel.None;

        /// <summary>
        /// A boolean flag indicating that the result is a failure
        /// </summary>
        public bool Failed
        {
            get { return !Succeeded; }
        }

        /// <summary>
        /// Set the message and the messageLevel
        /// </summary>
        /// <param name="message">the error message, may not be null or empty</param>
        /// <param name="level">the message level, may not be EMessageLevel.None</param>
        public void SetMessage(string message, EMessageLevel messageLevel = EMessageLevel.Info)
        {
            if (message.IsNullOrEmpty())
                throw new ArgumentNullException("message");
            if (messageLevel == EMessageLevel.None)
                throw new ArgumentNullException("messageLevel");
            Message = message;
            MessageLevel = messageLevel;
        }

        /// <summary>
        /// Clear the message and set the messageLevel to EMessageLevel.None
        /// This is not allowed for Failure results
        /// </summary>
        public void ClearMessage()
        {
            if (Failed)
                throw new NotSupportedException("Failure results must have a message and messageLevel");
            Message = null;
            MessageLevel = EMessageLevel.None;
        }

        /// <summary>
        /// The context in which the error occurred
        /// </summary>
        [DataMember]
        public TContext FailureContext { get; set; }

        /// <summary>
        /// The reason of the failure
        /// </summary>
        [DataMember]
        public TReason FailureReason { get; set; }
        #endregion

        #region constructor ---------------------------------------------------
        public ResultEx()
        {
            Succeeded = true;            
            FailureReason = default(TReason);
            FailureContext = default(TContext);
        }
        #endregion

        #region fluent methods ------------------------------------------------
        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IResultEx<TReason, TContext> OnFailure(Action<IResultEx<TReason, TContext>> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IResultEx<TReason, TContext> OnSuccess(Action<IResultEx<TReason, TContext>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Returns a new Result of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IResultEx<TReason, TContext> Continue(
            Func<IResultEx<TReason, TContext>, IResultEx<TReason, TContext>> continueFunc)
        {
            return continueFunc(this);
        }

        /// <summary>
        /// Converts to a ValueResult, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(TNew value)
        {
            return new ValueResultEx<TNew, TReason, TContext>
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
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(
            Func<IResultEx<TReason, TContext>, TNew> conversionFunc)
        {
            return new ValueResultEx<TNew, TReason, TContext>
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
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(
            Func<IValueResultEx<TNew, TReason, TContext>> successFunc,
            Func<IValueResultEx<TNew, TReason, TContext>> failureFunc = null)
        {
            return Succeeded ?
                successFunc() :
                failureFunc == null ? ConvertToValueResult(default(TNew)) : failureFunc();
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
        public IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(
            Func<IResultEx<TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> successFunc,
            Func<IResultEx<TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> failureFunc = null)
        {
            return Succeeded ?
                successFunc(this) :
                failureFunc == null ? ConvertToValueResult(default(TNew)) : failureFunc(this);
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
        public IEnumerable<IValueResultEx<TNew, TReason, TContext>> ConvertToValueResults<TNew>(
            Func<IEnumerable<IValueResultEx<TNew, TReason, TContext>>> successFunc,
            Func<IEnumerable<IValueResultEx<TNew, TReason, TContext>>> failureFunc = null)
        {
            return Succeeded ?
                successFunc() :
                failureFunc == null ? new[] { ConvertToValueResult(default(TNew)) } : failureFunc();
        }

        /// <summary>
        /// Returns an IEnumerable of ValueResults. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise an IEnumerable containing the default Value of TNew as only itemis returned 
        /// and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IEnumerable<IValueResultEx<TNew, TReason, TContext>> ConvertToValueResults<TNew>(
            Func<IResultEx<TReason, TContext>, IEnumerable<IValueResultEx<TNew, TReason, TContext>>> conversionFunc)
        {
            return conversionFunc(this);
        }
        #endregion
    }

    [DataContract]
    internal class Result<TReason> : ResultEx<TReason, string>, IResult<TReason>
    {
        #region constructor ---------------------------------------------------
        public Result() : base()
        {

        }

        #endregion       

        #region fluent methods ------------------------------------------------
        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IResult<TReason> OnFailure(Action<IResult<TReason>> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IResult<TReason> OnSuccess(Action<IResult<TReason>> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Returns a new Result of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IResult<TReason> Continue(
            Func<IResult<TReason>, IResult<TReason>> continueFunc)
        {
            return continueFunc(this);
        }

        /// <summary>
        /// Converts to a ValueResult, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public new IValueResult<TNew, TReason> ConvertToValueResult<TNew>(TNew value)
        {
            var result = new ValueResult<TNew, TReason>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                Value = value
            };
            result.SetMessage(Message, MessageLevel);
            return result;
        }

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResult<TNew, TReason> ConvertToValueResult<TNew>(
            Func<IResult<TReason>, TNew> conversionFunc)
        {
            var result = new ValueResult<TNew, TReason>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                Value = conversionFunc(this)
            };
            result.SetMessage(Message, MessageLevel);
            return result;
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
        public IValueResult<TNew, TReason> ConvertToValueResult<TNew>(
            Func<IValueResult<TNew, TReason>> successFunc,
            Func<IValueResult<TNew, TReason>> failureFunc = null)
        {
            return Succeeded ?
                successFunc() :
                failureFunc == null ? ConvertToValueResult(default(TNew)) : failureFunc();
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
        public IValueResult<TNew, TReason> ConvertToValueResult<TNew>(
            Func<IResult<TReason>, IValueResult<TNew, TReason>> successFunc,
            Func<IResult<TReason>, IValueResult<TNew, TReason>> failureFunc = null)
        {
            return Succeeded ?
                successFunc(this) :
                failureFunc == null ? ConvertToValueResult(default(TNew)) : failureFunc(this);
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
        public IEnumerable<IValueResult<TNew, TReason>> ConvertToValueResults<TNew>(
            Func<IEnumerable<IValueResult<TNew, TReason>>> successFunc,
            Func<IEnumerable<IValueResult<TNew, TReason>>> failureFunc = null)
        {
            return Succeeded ?
                successFunc() :
                failureFunc == null ? new[] { ConvertToValueResult(default(TNew)) } : failureFunc();
        }

        /// <summary>
        /// Returns an IEnumerable of ValueResults. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise an IEnumerable containing the default Value of TNew as only itemis returned 
        /// and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IEnumerable<IValueResult<TNew, TReason>> ConvertToValueResults<TNew>(
            Func<IResult<TReason>, IEnumerable<IValueResult<TNew, TReason>>> conversionFunc)
        {
            return conversionFunc(this);
        }
        #endregion
    }

    [DataContract]
    internal class Result : Result<int>, IResult
    {

        #region constructor ---------------------------------------------------
        public Result() : base()
        {

        }

        #endregion

        #region fluent methods ------------------------------------------------
        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        [DebuggerStepThrough]
        public IResult OnFailure(Action<IResult> failureAction)
        {
            if (!Succeeded) failureAction(this);
            return this;
        }

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IResult OnSuccess(Action<IResult> successAction)
        {
            if (Succeeded) successAction(this);
            return this;
        }

        /// <summary>
        /// Returns a new Result of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IResult Continue(
            Func<IResult, IResult> continueFunc)
        {
            return continueFunc(this);
        }

        /// <summary>
        /// Converts to a ValueResult, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public new IValueResult<TNew> ConvertToValueResult<TNew>(TNew value)
        {
            var result = new ValueResult<TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,
                Value = value
            };
            result.SetMessage(Message, MessageLevel);
            return result;
        }

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IValueResult<TNew> ConvertToValueResult<TNew>(
            Func<IResult, TNew> conversionFunc)
        {
            var result = new ValueResult<TNew>
            {
                Succeeded = Succeeded,
                FailureReason = FailureReason,                
                Value = conversionFunc(this)
            };
            result.SetMessage(Message, MessageLevel);
            return result;
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
        public IValueResult<TNew> ConvertToValueResult<TNew>(
            Func<IValueResult<TNew>> successFunc,
            Func<IValueResult<TNew>> failureFunc = null)
        {
            return Succeeded ?
                successFunc() :
                failureFunc == null ? ConvertToValueResult(default(TNew)) : failureFunc();
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
        public IValueResult<TNew> ConvertToValueResult<TNew>(
            Func<IResult, IValueResult<TNew>> successFunc,
            Func<IResult, IValueResult<TNew>> failureFunc = null)
        {
            return Succeeded ?
                successFunc(this) :
                failureFunc == null ? ConvertToValueResult(default(TNew)) : failureFunc(this);
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
        public IEnumerable<IValueResult<TNew>> ConvertToValueResults<TNew>(
            Func<IEnumerable<IValueResult<TNew>>> successFunc,
            Func<IEnumerable<IValueResult<TNew>>> failureFunc = null)
        {
            return Succeeded ?
                successFunc() :
                failureFunc == null ? new[] { ConvertToValueResult(default(TNew)) } : failureFunc();
        }

        /// <summary>
        /// Returns an IEnumerable of ValueResults. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise an IEnumerable containing the default Value of TNew as only itemis returned 
        /// and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public IEnumerable<IValueResult<TNew>> ConvertToValueResults<TNew>(
            Func<IResult, IEnumerable<IValueResult<TNew>>> conversionFunc)
        {
            return conversionFunc(this);
        }
        #endregion
    }
}