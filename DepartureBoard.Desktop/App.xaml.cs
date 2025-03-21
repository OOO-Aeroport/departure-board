using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace DepartureBoard.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        SingletonHttpClient.BaseAddress = "http://26.228.200.110:5555/";
    }
}