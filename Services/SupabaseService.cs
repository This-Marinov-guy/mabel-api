using Supabase;

namespace MabelApi.Services;

public class SupabaseService : ISupabaseService
{
    private readonly Client _client;

    public SupabaseService(IConfiguration configuration)
    {
        var url = configuration["Supabase:Url"] 
            ?? throw new ArgumentNullException("Supabase:Url", "Supabase URL is not configured");
        var key = configuration["Supabase:Key"] 
            ?? throw new ArgumentNullException("Supabase:Key", "Supabase Key is not configured");

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        _client = new Client(url, key, options);
    }

    public Client GetClient()
    {
        return _client;
    }
}

