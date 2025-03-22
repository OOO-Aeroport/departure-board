using System.IO;

namespace DepartureBoard.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        var root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
        var path = Path.Combine(root, "DepartureBoardBaseAddress.txt");
        
        if (!File.Exists(path)) throw new FileNotFoundException();
        
        SingletonHttpClient.BaseAddress = File.ReadAllText(path);
    }
}