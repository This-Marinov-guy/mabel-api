using Supabase;

namespace MabelApi.Services;

public interface ISupabaseService
{
    Client GetClient();
}

