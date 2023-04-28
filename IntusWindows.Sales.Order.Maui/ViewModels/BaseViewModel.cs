using System;
namespace IntusWindows.Sales.Order.Maui.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public BaseViewModel()
    {

    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (!Equals(field, value))
        {
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        return false;
    }

    private bool _isRunning;

    public bool IsRunning
    {
        get { return _isRunning; }
        set { Set(ref _isRunning, value); }
    }
}

