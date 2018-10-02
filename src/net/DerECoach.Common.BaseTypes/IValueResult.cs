using System;
using System.Collections.Generic;

namespace DerECoach.Common.BaseTypes
{
    public interface IBaseValueResult<TValue>: IBaseResult
    {
        TValue Value { get; set; }
    }

    public interface IReasonValueResult<TReason, TValue> : IBaseValueResult<TValue>
    {
        /// <summary>
        /// The reason of the failure
        /// </summary>
        TReason FailureReason { get; set; }
    }

    public interface IValueResult<TValue> : IBaseValueResult<TValue>
    {      
        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        IValueResult<TValue> OnFailure(
            Action<IValueResult<TValue>> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        IValueResult<TValue> OnSuccess(
            Action<IValueResult<TValue>> successAction);

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IValueResult<TValue> Continue(
            Func<IValueResult<TValue>, IValueResult<TValue>> continuefunc);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResult<TNew> Convert<TNew>(
            Func<IValueResult<TValue>, TNew> conversionFunc);

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResult<TNew> Convert<TNew>(TNew value);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserverd
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResult<TNew> Convert<TNew>(
            Func<IValueResult<TValue>, IValueResult<TNew>> successFunc,
            Func<IValueResult<TValue>, IValueResult<TNew>> failureFunc = null);

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
        IEnumerable<IValueResult<TNew>> Convert<TNew>(
            Func<IValueResult<TValue>, IEnumerable<IValueResult<TNew>>> successFunc,
            Func<IValueResult<TValue>, IEnumerable<IValueResult<TNew>>> failureFunc = null);

        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        IValueResult<TNew> Cast<TNew>() where TNew : class;
        #endregion
    }

    public interface IValueResult<TReason, TValue> : IReasonValueResult<TReason, TValue>
    {
        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        IValueResult<TReason, TValue> OnFailure(
            Action<IValueResult<TReason, TValue>> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        IValueResult<TReason, TValue> OnSuccess(
            Action<IValueResult<TReason, TValue>> successAction);

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IValueResult<TReason, TValue> Continue(
            Func<IValueResult<TReason, TValue>, IValueResult<TReason, TValue>> continuefunc);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResult<TReason, TNew> Convert<TNew>(
            Func<IValueResult<TReason, TValue>, TNew> conversionFunc);

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResult<TReason, TNew> Convert<TNew>(TNew value);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserverd
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResult<TReason, TNew> Convert<TNew>(
            Func<IValueResult<TReason, TValue>, IValueResult<TReason, TNew>> successFunc,
            Func<IValueResult<TReason, TValue>, IValueResult<TReason, TNew>> failureFunc = null);

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
        IEnumerable<IValueResult<TReason, TNew>> Convert<TNew>(
            Func<IValueResult<TReason, TValue>, IEnumerable<IValueResult<TReason, TNew>>> successFunc,
            Func<IValueResult<TReason, TValue>, IEnumerable<IValueResult<TReason, TNew>>> failureFunc = null);

        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        IValueResult<TReason, TNew> Cast<TNew>() where TNew : class;
        #endregion
    }

    public interface IValueResultEx<TReason, TContext, TValue>: IReasonValueResult<TReason, TValue>
    {
        /// <summary>
        /// The context in which the error occurred
        /// </summary>
        TContext FailureContext { get; set; }

        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        IValueResultEx<TReason, TContext, TValue> OnFailure(
            Action<IValueResultEx<TReason, TContext, TValue>> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        IValueResultEx<TReason, TContext, TValue> OnSuccess(
            Action<IValueResultEx<TReason, TContext, TValue>> successAction);

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IValueResultEx<TReason, TContext, TValue> Continue(
            Func<IValueResultEx<TReason, TContext, TValue>, IValueResultEx<TReason, TContext, TValue>> continuefunc);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResultEx<TReason, TContext, TNew> Convert<TNew>(
            Func<IValueResultEx<TReason, TContext, TValue>, TNew> conversionFunc);

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResultEx<TReason, TContext, TNew> Convert<TNew>(TNew value);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserverd
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResultEx<TReason, TContext, TNew> Convert<TNew>(
            Func<IValueResultEx<TReason, TContext, TValue>, IValueResultEx<TReason, TContext, TNew>> successFunc,
            Func<IValueResultEx<TReason, TContext, TValue>, IValueResultEx<TReason, TContext, TNew>> failureFunc = null);

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
        IEnumerable<IValueResultEx<TReason, TContext, TNew>> Convert<TNew>(
            Func<IValueResultEx<TReason, TContext, TValue>, IEnumerable<IValueResultEx<TReason, TContext, TNew>>> successFunc,
            Func<IValueResultEx<TReason, TContext, TValue>, IEnumerable<IValueResultEx<TReason, TContext, TNew>>> failureFunc = null);

        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        IValueResultEx<TReason, TContext, TNew> Cast<TNew>() where TNew : class;
        #endregion
    }
}