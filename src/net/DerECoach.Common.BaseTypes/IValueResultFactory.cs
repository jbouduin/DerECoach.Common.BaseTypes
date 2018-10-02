namespace DerECoach.Common.BaseTypes
{
    public interface IValueResultFactory
    {
        IValueResultEx<TReason, TContext, TValue> Failure<TReason, TContext, TValue>(TValue value, string message);
        IValueResultEx<TReason, TContext, TValue> Failure<TReason, TContext, TValue>(TValue value, TReason failureReason, string message);
        IValueResultEx<TReason, TContext, TValue> Failure<TReason, TContext, TValue>(TValue value, TReason failureReason, TContext failureContext, string message);
        IValueResult<TReason, TValue> Failure<TReason, TValue>(TValue value, string message);
        IValueResult<TReason, TValue> Failure<TReason, TValue>(TValue value, TReason failureReason, string message);
        IValueResult<TValue> Failure<TValue>(TValue value, string message);
        IValueResultEx<TReason, TContext, TValue> Success<TReason, TContext, TValue>(TValue value);
        IValueResultEx<TReason, TContext, TValue> Success<TReason, TContext, TValue>(TValue value, string message, EMessageLevel messageLevel);
        IValueResult<TReason, TValue> Success<TReason, TValue>(TValue value);
        IValueResult<TReason, TValue> Success<TReason, TValue>(TValue value, string message, EMessageLevel messageLevel);
        IValueResult<TValue> Success<TValue>(TValue value);
        IValueResult<TValue> Success<TValue>(TValue value, string message, EMessageLevel messageLevel);
    }
}