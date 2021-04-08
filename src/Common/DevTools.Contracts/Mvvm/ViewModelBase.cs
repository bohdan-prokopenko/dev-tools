namespace DevTools.Contracts.Mvvm
{
    using System.ComponentModel;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
           => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
