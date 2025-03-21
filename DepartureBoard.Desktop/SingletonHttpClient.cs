using System.Net.Http;

namespace DepartureBoard.Desktop;

public class SingletonHttpClient
{
    private readonly HttpClient _client;
    private static SingletonHttpClient? _instance;
    private static readonly Lock Lock = new Lock();
    
    public static SingletonHttpClient Instance
    {
        get
        {
            lock (Lock)
            {
                _instance ??= new SingletonHttpClient(new HttpClient
                {
                    BaseAddress = new Uri(BaseAddress ?? string.Empty)
                });
                return _instance;
            }
        }
    }
    public static string? BaseAddress { get; set; }
    
    public async Task SendChangeSpt(string spt)
    {
        await _client.PutAsync($"dep-board/api/v1/time/spt?spt={spt}", null);
    }

    public async Task<string> SendGetTime()
        => await _client.GetStringAsync("dep-board/api/v1/time/now");

    private SingletonHttpClient(HttpClient client)
    {
        _client = client;
    }
}