using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DerECoach.Common.BaseTypes
{
    /// <summary>
    /// an abstract base class implementing INotifyPropertyChanged
    /// </summary>
    public abstract class APropertyChangedNotifier : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged ----------------------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Property changed methods --------------------------------------

        /// <summary>
        /// the method triggering the PropertyChangedEventhandler indirectly
        /// </summary>
        /// <param name="selectorExpression">An expression used to extract the name of the property
        /// e.g. () => Name</param>
        protected virtual void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> selectorExpression)
        {
            PropertyChanged.FirePropertyChanged(this, selectorExpression);
        }

        /// <summary>
        /// the method triggering the PropertyChangedEventhandler indirectly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectorExpression">An expression used to extract the name of the property
        /// e.g. () => Name</param>
        protected virtual void OnPropertyChanged<TSender, TProperty>(
            TSender sender,
            Expression<Func<TSender, TProperty>> selectorExpression)
        {
            PropertyChanged.FirePropertyChanged(sender, selectorExpression);
        }

        #endregion
    }
}