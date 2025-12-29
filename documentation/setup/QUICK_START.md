# Quick Start Guide

Get your Mabel API up and running in minutes!

## 🚀 Quick Setup (5 minutes)

### 1. Configure Supabase (2 minutes)

1. Create a free account at [supabase.com](https://supabase.com)
2. Create a new project
3. Copy your **Project URL** and **anon key** from Settings → API
4. Update `appsettings.Development.json`:

```json
{
  "Supabase": {
    "Url": "YOUR_PROJECT_URL_HERE",
    "Key": "YOUR_ANON_KEY_HERE"
  }
}
```

### 2. Create Database Table (1 minute)

In Supabase SQL Editor, run:

```sql
CREATE TABLE todos (
  id SERIAL PRIMARY KEY,
  title TEXT NOT NULL,
  description TEXT,
  is_completed BOOLEAN DEFAULT FALSE,
  created_at TIMESTAMPTZ DEFAULT NOW(),
  updated_at TIMESTAMPTZ DEFAULT NOW()
);

ALTER TABLE todos ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Allow all operations" ON todos
  FOR ALL USING (true) WITH CHECK (true);
```

### 3. Run the API (30 seconds)

```bash
cd MabelApi
dotnet run
```

### 4. Test It! (30 seconds)

Open another terminal and try:

```bash
# Create a todo
curl -X POST https://localhost:7XXX/api/todo \
  -H "Content-Type: application/json" \
  -d '{"title":"My First Todo","isCompleted":false}'

# Get all todos
curl https://localhost:7XXX/api/todo
```

Replace `7XXX` with the actual port shown in your console.

## 📚 What's Included

- ✅ Full CRUD API for Todos
- ✅ Supabase integration
- ✅ OpenAPI/Swagger support
- ✅ Error handling
- ✅ Logging configured

## 📖 Next Steps

- Read [SUPABASE_SETUP.md](./SUPABASE_SETUP.md) for detailed Supabase configuration
- Read [README.md](../README.md) for complete API documentation
- Check out the example controller at `Controllers/TodoController.cs`

## 🆘 Need Help?

**API not starting?**
- Make sure .NET 9 SDK is installed: `dotnet --version`
- Check port conflicts in `Properties/launchSettings.json`

**Can't connect to Supabase?**
- Verify your URL and key are correct
- Check that the todos table exists
- Ensure RLS policy is created

**Getting 404 errors?**
- Make sure you're using the correct port
- Verify the endpoint path: `/api/todo`

## 🎯 API Endpoints

| Method | Endpoint | Action |
|--------|----------|--------|
| GET | `/api/todo` | List all |
| GET | `/api/todo/{id}` | Get one |
| POST | `/api/todo` | Create |
| PUT | `/api/todo/{id}` | Update |
| DELETE | `/api/todo/{id}` | Delete |

Happy coding! 🎉

