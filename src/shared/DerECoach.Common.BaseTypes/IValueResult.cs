using System;
using System.Collections.Generic;

namespace DerECoach.Common.BaseTypes
{
    public interface IBaseValueResult<TValue>: IBaseResult
    {
        TValue Value { get; set; }
    }

    public interface IReasonValueResult<TValue, TReason> : IBaseValueResult<TValue>
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

    public interface IValueResult<TValue, TReason> : IReasonValueResult<TValue, TReason>
    {
        #region fluent methods ------------------------------------------------

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        IValueResult<TValue, TReason> OnFailure(
            Action<IValueResult<TValue, TReason>> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        IValueResult<TValue, TReason> OnSuccess(
            Action<IValueResult<TValue, TReason>> successAction);

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IValueResult<TValue, TReason> Continue(
            Func<IValueResult<TValue, TReason>, IValueResult<TValue, TReason>> continuefunc);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResult<TNew, TReason> Convert<TNew>(
            Func<IValueResult<TValue, TReason>, TNew> conversionFunc);

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResult<TNew, TReason> Convert<TNew>(TNew value);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserverd
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResult<TNew, TReason> Convert<TNew>(
            Func<IValueResult<TValue, TReason>, IValueResult<TNew, TReason>> successFunc,
            Func<IValueResult<TValue, TReason>, IValueResult<TNew, TReason>> failureFunc = null);

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
        IEnumerable<IValueResult<TNew, TReason>> Convert<TNew>(
            Func<IValueResult<TValue, TReason>, IEnumerable<IValueResult<TNew, TReason>>> successFunc,
            Func<IValueResult<TValue, TReason>, IEnumerable<IValueResult<TNew, TReason>>> failureFunc = null);

        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        IValueResult<TNew, TReason> Cast<TNew>() where TNew : class;
        #endregion
    }

    public interface IValueResultEx<TValue, TReason, TContext>: IReasonValueResult<TValue, TReason>
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
        IValueResultEx<TValue, TReason, TContext> OnFailure(
            Action<IValueResultEx<TValue, TReason, TContext>> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return the result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns>this</returns>
        IValueResultEx<TValue, TReason, TContext> OnSuccess(
            Action<IValueResultEx<TValue, TReason, TContext>> successAction);

        /// <summary>
        /// Returns a new ValueResult of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IValueResultEx<TValue, TReason, TContext> Continue(
            Func<IValueResultEx<TValue, TReason, TContext>, IValueResultEx<TValue, TReason, TContext>> continuefunc);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> Convert<TNew>(
            Func<IValueResultEx<TValue, TReason, TContext>, TNew> conversionFunc);

        /// <summary>
        /// Converts to a ValueResult of another type, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> Convert<TNew>(TNew value);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserverd
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> Convert<TNew>(
            Func<IValueResultEx<TValue, TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> successFunc,
            Func<IValueResultEx<TValue, TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> failureFunc = null);

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
        IEnumerable<IValueResultEx<TNew, TReason, TContext>> Convert<TNew>(
            Func<IValueResultEx<TValue, TReason, TContext>, IEnumerable<IValueResultEx<TNew, TReason, TContext>>> successFunc,
            Func<IValueResultEx<TValue, TReason, TContext>, IEnumerable<IValueResultEx<TNew, TReason, TContext>>> failureFunc = null);

        /// <summary>
        /// Casts the Value to a new Type. All other properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> Cast<TNew>() where TNew : class;
        #endregion
    }
}