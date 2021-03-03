namespace DevTools.Infrastructure.DataContext
{
    using System.ComponentModel;

    public abstract class DataContextBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
