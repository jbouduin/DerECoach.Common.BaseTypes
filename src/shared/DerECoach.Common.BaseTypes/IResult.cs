using System;
using System.Collections.Generic;

namespace DerECoach.Common.BaseTypes
{
    public interface IBaseResult
    {
        /// <summary>
        /// A boolean flag indicating that the result is a success
        /// </summary>
        bool Succeeded { get; set; }

        /// <summary>
        /// The result message. If the result is a failure, this message is mandatory.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// The Error level message
        /// </summary>
        EMessageLevel MessageLevel { get; }

        /// <summary>
        /// A boolean flag indicating that the result is a failure
        /// </summary>
        bool Failed { get; }

        /// <summary>
        /// Set the message and the messageLevel
        /// </summary>
        /// <param name="message">the error message, may not be null or empty</param>
        /// <param name="level">the message level, may not be EMessageLevel.None</param>
        void SetMessage(string message, EMessageLevel messageLevel = EMessageLevel.None);

        void ClearMessage();

    }

    public interface IResult: IBaseResult
    {
        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        IResult OnFailure(Action<IResult> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns></returns>
        IResult OnSuccess(Action<IResult> successAction);

        /// <summary>
        /// Returns a new Result of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IResult Continue(
            Func<IResult, IResult> continueFunc);

        /// <summary>
        /// Converts to a ValueResult, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResult<TNew> ConvertToValueResult<TNew>(TNew value);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResult<TNew> ConvertToValueResult<TNew>(
            Func<IResult, TNew> conversionFunc);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResult<TNew> ConvertToValueResult<TNew>(
            Func<IValueResult<TNew>> successFunc,
            Func<IValueResult<TNew>> failureFunc = null);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResult<TNew> ConvertToValueResult<TNew>(
            Func<IResult, IValueResult<TNew>> successFunc,
            Func<IResult, IValueResult<TNew>> failureFunc = null);

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
        IEnumerable<IValueResult<TNew>> ConvertToValueResults<TNew>(
            Func<IEnumerable<IValueResult<TNew>>> successFunc,
            Func<IEnumerable<IValueResult<TNew>>> failureFunc = null);

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
        IEnumerable<IValueResult<TNew>> ConvertToValueResults<TNew>(
            Func<IResult, IEnumerable<IValueResult<TNew>>> conversionFunc);


    }

    public interface IReasonResult<TReason>: IBaseResult
    {
        /// <summary>
        /// The reason of the failure
        /// </summary>
        TReason FailureReason { get; set; }
    }

    public interface IResult<TReason>: IReasonResult<TReason>
    {
        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        IResult<TReason> OnFailure(Action<IResult<TReason>> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns></returns>
        IResult<TReason> OnSuccess(Action<IResult<TReason>> successAction);

        /// <summary>
        /// Returns a new Result of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IResult<TReason> Continue(
            Func<IResult<TReason>, IResult<TReason>> continueFunc);

        /// <summary>
        /// Converts to a ValueResult, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResult<TNew, TReason> ConvertToValueResult<TNew>(TNew value);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResult<TNew, TReason> ConvertToValueResult<TNew>(
            Func<IResult<TReason>, TNew> conversionFunc);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResult<TNew, TReason> ConvertToValueResult<TNew>(
            Func<IValueResult<TNew, TReason>> successFunc,
            Func<IValueResult<TNew, TReason>> failureFunc = null);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResult<TNew, TReason> ConvertToValueResult<TNew>(
            Func<IResult<TReason>, IValueResult<TNew, TReason>> successFunc,
            Func<IResult<TReason>, IValueResult<TNew, TReason>> failureFunc = null);

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
        IEnumerable<IValueResult<TNew, TReason>> ConvertToValueResults<TNew>(
            Func<IEnumerable<IValueResult<TNew, TReason>>> successFunc,
            Func<IEnumerable<IValueResult<TNew, TReason>>> failureFunc = null);

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
        IEnumerable<IValueResult<TNew, TReason>> ConvertToValueResults<TNew>(
            Func<IResult<TReason>, IEnumerable<IValueResult<TNew, TReason>>> conversionFunc);
    }

    public interface IResultEx<TReason, TContext>: IReasonResult<TReason>
    {
        /// <summary>
        /// The context in which the error occurred
        /// </summary>
        TContext FailureContext { get; set; }

        /// <summary>
        /// Perform the provided Action if the result is Failed and return the result
        /// </summary>
        /// <param name="failureAction"></param>
        /// <returns>this</returns>
        IResultEx<TReason, TContext> OnFailure(Action<IResultEx<TReason, TContext>> failureAction);

        /// <summary>
        /// Perform the provided Action if the result is Succeeded and return result
        /// </summary>
        /// <param name="successAction"></param>
        /// <returns></returns>
        IResultEx<TReason, TContext> OnSuccess(Action<IResultEx<TReason, TContext>> successAction);

        /// <summary>
        /// Returns a new Result of the same type. 
        /// </summary>
        /// <param name="continueFunc"></param>
        /// <returns></returns>
        IResultEx<TReason, TContext> Continue(
            Func<IResultEx<TReason, TContext>, IResultEx<TReason, TContext>> continueFunc);

        /// <summary>
        /// Converts to a ValueResult, assigning the passed value to Value
        /// All properties are preserved
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="value">the new Value</param>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(TNew value);

        /// <summary>
        /// Convert to a ValueResult, setting the value using the conversionFunc
        /// All properties are preserved 
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="conversionFunc"></param>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(
            Func<IResultEx<TReason, TContext>, TNew> conversionFunc);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(
            Func<IValueResultEx<TNew, TReason, TContext>> successFunc,
            Func<IValueResultEx<TNew, TReason, TContext>> failureFunc = null);

        /// <summary>
        /// Returns a new Result. 
        /// If Succeeded the return value of successFunc is returned.
        /// If Failed and failureFunc is set, the return value of failureFunc is returned,
        /// otherwise the default Value of TNew is returned and all other properties are preserved
        /// </summary>
        /// <param name="successFunc"></param>
        /// <param name="failureFunc"></param>
        /// <returns></returns>
        IValueResultEx<TNew, TReason, TContext> ConvertToValueResult<TNew>(
            Func<IResultEx<TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> successFunc,
            Func<IResultEx<TReason, TContext>, IValueResultEx<TNew, TReason, TContext>> failureFunc = null);

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
        IEnumerable<IValueResultEx<TNew, TReason, TContext>> ConvertToValueResults<TNew>(
            Func<IEnumerable<IValueResultEx<TNew, TReason, TContext>>> successFunc,
            Func<IEnumerable<IValueResultEx<TNew, TReason, TContext>>> failureFunc = null);

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
        IEnumerable<IValueResultEx<TNew, TReason, TContext>> ConvertToValueResults<TNew>(
            Func<IResultEx<TReason, TContext>, IEnumerable<IValueResultEx<TNew, TReason, TContext>>> conversionFunc);
        
    }
}