using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Stepometer.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected bool IsBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
