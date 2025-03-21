using System.ComponentModel;
using System.Windows.Input;

namespace DepartureBoard.Desktop;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _sptContent = string.Empty;
    
    public string SptContent
    {
        get => _sptContent;
        set
        {
            _sptContent = value;
            OnPropertyChanged(nameof(SptContent));
        }
    }

    private string _timeContent = string.Empty;

    public string TimeContent
    {
        get => _timeContent;
        set
        {
            _timeContent = value;
            OnPropertyChanged(nameof(TimeContent));
        }
    }

    public ICommand ChangeSptCommand { get; }
    
    public MainWindowViewModel()
    {
        ChangeSptCommand = new RelayCommand(ChangeSpt);

        Task.Run(StartTimer);
    }

    private async Task StartTimer()
    {
        while (true)
        {
            await GetTime();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    private async Task ChangeSpt()
    {
        await SingletonHttpClient.Instance.SendChangeSpt(SptContent);
    }

    private async Task GetTime()
    {
        TimeContent = (await SingletonHttpClient.Instance.SendGetTime())
            .Trim('"');
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}