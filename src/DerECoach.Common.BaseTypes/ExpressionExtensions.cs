using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DerECoach.Common.BaseTypes
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<TContainingObject, TResult>(this Expression<Func<TContainingObject, TResult>> expression)
        {
            var memberExpression = expression.Body as MemberExpression ?? (MemberExpression)((UnaryExpression)expression.Body).Operand;
            return memberExpression.Member.Name;
        }

        public static PropertyInfo GetPropertyInfo<TContainingObject, TResult>(this Expression<Func<TContainingObject, TResult>> expression)
        {
            var memberExpression = expression.Body as MemberExpression ?? (MemberExpression)((UnaryExpression)expression.Body).Operand;
            return (PropertyInfo)memberExpression.Member;
        }

        public static void CheckIsProperty(this Expression property, MemberExpression memberExpression)
        {
            if (memberExpression.Member.MemberType != MemberTypes.Property)
            {
                ThrowNotValidLambda(property);
            }
        }

        public static T ThrowOnNull<T>(this Expression property, T lambda) where T : class
        {
            if (lambda == null)
            {
                ThrowNotValidLambda(property);
            }
            return lambda;
        }

        private static void ThrowNotValidLambda(Expression property)
        {
            throw new ArgumentException(string.Format("Invalid property expression: {0}", property));
        }
    }
}