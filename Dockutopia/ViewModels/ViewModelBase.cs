using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dockutopia.ViewModels
{


    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public void Dispose()
        {
            OnDispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnDispose()
        {
        }




    }
}
