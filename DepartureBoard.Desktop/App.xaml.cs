namespace DepartureBoard.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        SingletonHttpClient.BaseAddress = "http://26.228.200.110:5555/";
    }
}