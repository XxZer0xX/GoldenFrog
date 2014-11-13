﻿namespace LoginDialog.Bindings
{
    #region Referenceing

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    #endregion

    /// <summary>
    ///     ViewModel base
    /// </summary>
    internal class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected virtual void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;
            field = value;

            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}