using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DerECoach.Common.BaseTypes
{
    public class APropertyChangedNotifierWithTracking : APropertyChangedNotifier
    {

        #region fields --------------------------------------------------------

        private readonly Dictionary<string, object> _dictionaryOfChanges = new Dictionary<string, object>();

        #endregion

        #region properties ----------------------------------------------------

        public virtual bool HasChanges
        {
            get { return _dictionaryOfChanges.Any(); }
        }

        public IEnumerable<string> ChangedPropertyNames
        {
            get { return _dictionaryOfChanges.Keys; }
        }

        #endregion

        #region Property changed methods --------------------------------------

        /// <summary>
        /// the method triggering the PropertyChangedEventhandler indirectly and tracking value changes
        /// </summary>
        /// <param name="selectorExpression">An expression used to extract the name of the property
        /// e.g. () => Name</param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnPropertyChanged<TValue, TProperty>(
            Expression<Func<TProperty>> selectorExpression,
            TValue oldValue,
            TValue newValue)
        {

            var lambda = selectorExpression.ThrowOnNull(selectorExpression as LambdaExpression);
            var call = selectorExpression.ThrowOnNull(lambda.Body as MemberExpression);

            selectorExpression.CheckIsProperty(call);
            HandleChangeTracking(call.Member.Name, oldValue, newValue);

            OnPropertyChanged(selectorExpression);
            OnPropertyChanged(() => HasChanges);
        }

        /// <summary>
        /// the method triggering the PropertyChangedEventhandler indirectly and tracking value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectorExpression">An expression used to extract the name of the property
        /// e.g. () => Name</param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void OnPropertyChanged<TValue, TSender, TProperty>(
            TSender sender,
            Expression<Func<TSender, TProperty>> selectorExpression,
            TValue oldValue,
            TValue newValue)
        {

            var lambda = selectorExpression.ThrowOnNull(selectorExpression as LambdaExpression);
            var call = selectorExpression.ThrowOnNull(lambda.Body as MemberExpression);

            selectorExpression.CheckIsProperty(call);
            HandleChangeTracking(call.Member.Name, oldValue, newValue);

            OnPropertyChanged(sender, selectorExpression);
            OnPropertyChanged(() => HasChanges);
        }

        /// <summary>
        /// the method triggering the PropertyChangedEventhandler indirectly and tracking value changes
        /// </summary>
        /// <param name="selectorExpression">An expression used to extract the name of the property
        /// e.g. () => Name</param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="nullEqualsEmpty"></param>
        /// <param name="stringComparison"></param>
        protected virtual void OnStringPropertyChanged<TProperty>(
            Expression<Func<TProperty>> selectorExpression,
            string oldValue,
            string newValue,
            bool nullEqualsEmpty = true,
            StringComparison stringComparison = StringComparison.InvariantCulture)
        {

            var lambda = selectorExpression.ThrowOnNull(selectorExpression as LambdaExpression);
            var call = selectorExpression.ThrowOnNull(lambda.Body as MemberExpression);
            selectorExpression.CheckIsProperty(call);

            HandleStringChangeTracking(call.Member.Name, nullEqualsEmpty, oldValue, newValue, stringComparison);

            OnPropertyChanged(selectorExpression);
            OnPropertyChanged(() => HasChanges);
        }

        /// <summary>
        /// the method triggering the PropertyChangedEventhandler indirectly and tracking value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectorExpression">An expression used to extract the name of the property
        /// e.g. () => Name</param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="nullEqualsEmpty"></param>
        /// <param name="stringComparison"></param>
        protected virtual void OnStringPropertyChanged<TSender, TProperty>(
            TSender sender,
            Expression<Func<TSender, TProperty>> selectorExpression,
            string oldValue,
            string newValue,
            bool nullEqualsEmpty = true,
            StringComparison stringComparison = StringComparison.InvariantCulture)
        {

            var lambda = selectorExpression.ThrowOnNull(selectorExpression as LambdaExpression);
            var call = selectorExpression.ThrowOnNull(lambda.Body as MemberExpression);
            selectorExpression.CheckIsProperty(call);

            HandleStringChangeTracking(call.Member.Name, nullEqualsEmpty, oldValue, newValue, stringComparison);

            OnPropertyChanged(sender, selectorExpression);
            OnPropertyChanged(() => HasChanges);
        }

        #endregion

        #region other public methods ------------------------------------------

        public virtual void ClearChangeFlag()
        {
            _dictionaryOfChanges.Clear();
        }

        #endregion

        #region helper methods ------------------------------------------------

        private void HandleChangeTracking<TValue>(string propertyName, TValue oldValue, TValue newValue)
        {
            if (!oldValue.EqualsConsideringNull(newValue))
            {

                if (!_dictionaryOfChanges.ContainsKey(propertyName))
                {
                    _dictionaryOfChanges.Add(propertyName, oldValue);
                }
                else
                {
                    if (_dictionaryOfChanges[propertyName].EqualsConsideringNull(newValue))
                    {
                        _dictionaryOfChanges.Remove(propertyName);
                    }
                }
            }
        }

        private void HandleStringChangeTracking(
            string propertyName,
            bool nullEqualsEmpty,
            string oldValue,
            string newValue,
            StringComparison stringComparison)
        {
            var valueChange = nullEqualsEmpty
                ? !oldValue.EqualsEmptyEqualsNull(newValue, stringComparison)
                : oldValue.EqualsConsideringNull(newValue, stringComparison);
            if (valueChange)
            {
                if (!_dictionaryOfChanges.ContainsKey(propertyName))
                {
                    _dictionaryOfChanges.Add(propertyName, oldValue);
                }
                else
                {
                    var backToOriginal = nullEqualsEmpty
                        ? newValue.EqualsEmptyEqualsNull(_dictionaryOfChanges[propertyName] as string,
                            stringComparison)
                        : newValue.EqualsConsideringNull(_dictionaryOfChanges[propertyName] as string,
                            stringComparison);
                    if (backToOriginal)
                    {
                        _dictionaryOfChanges.Remove(propertyName);
                    }
                }
            }
        }

        #endregion
    }

}