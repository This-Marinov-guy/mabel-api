# Mabel API - .NET Web API with Supabase

A modern .NET 9 Web API integrated with Supabase for backend data management.

## Features

- ✅ .NET 9 Web API with Controllers
- ✅ Supabase integration for database operations
- ✅ RESTful API endpoints
- ✅ Example Todo CRUD operations
- ✅ Dependency injection configured
- ✅ OpenAPI/Swagger support

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Supabase Account](https://supabase.com/) (free tier available)

## Setup Instructions

### 1. Clone and Navigate to Project

```bash
cd /Users/vlady/Desktop/mabel-api/MabelApi
```

### 2. Configure Supabase

1. Create a Supabase project at [https://supabase.com](https://supabase.com)
2. Get your project URL and anon key from the Supabase dashboard (Settings > API)
3. Update the configuration in `appsettings.Development.json`:

```json
{
  "Supabase": {
    "Url": "https://your-project.supabase.co",
    "Key": "your-anon-key-here"
  }
}
```

### 3. Create Database Table

Run this SQL in your Supabase SQL Editor to create the todos table:

```sql
CREATE TABLE todos (
  id SERIAL PRIMARY KEY,
  title TEXT NOT NULL,
  description TEXT,
  is_completed BOOLEAN DEFAULT FALSE,
  created_at TIMESTAMPTZ DEFAULT NOW(),
  updated_at TIMESTAMPTZ DEFAULT NOW()
);

-- Enable Row Level Security (optional but recommended)
ALTER TABLE todos ENABLE ROW LEVEL SECURITY;

-- Create a policy to allow all operations (adjust based on your needs)
CREATE POLICY "Allow all operations" ON todos
  FOR ALL
  USING (true)
  WITH CHECK (true);
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7XXX` (check console output for exact port)
- HTTP: `http://localhost:5XXX`

## API Endpoints

### Todo Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todo` | Get all todos |
| GET | `/api/todo/{id}` | Get a specific todo by ID |
| POST | `/api/todo` | Create a new todo |
| PUT | `/api/todo/{id}` | Update an existing todo |
| DELETE | `/api/todo/{id}` | Delete a todo |

### Example Requests

#### Create a Todo (POST)

```bash
curl -X POST https://localhost:7XXX/api/todo \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Learn .NET",
    "description": "Study .NET 9 features",
    "isCompleted": false
  }'
```

#### Get All Todos (GET)

```bash
curl https://localhost:7XXX/api/todo
```

#### Update a Todo (PUT)

```bash
curl -X PUT https://localhost:7XXX/api/todo/1 \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Learn .NET",
    "description": "Study .NET 9 features",
    "isCompleted": true
  }'
```

#### Delete a Todo (DELETE)

```bash
curl -X DELETE https://localhost:7XXX/api/todo/1
```

## Project Structure

```
MabelApi/
├── Controllers/
│   ├── TodoController.cs          # Todo CRUD endpoints
│   └── WeatherForecastController.cs # Default example controller
├── Models/
│   └── TodoItem.cs                # Todo data model
├── Services/
│   ├── ISupabaseService.cs        # Supabase service interface
│   └── SupabaseService.cs         # Supabase client implementation
├── Properties/
│   └── launchSettings.json        # Launch configuration
├── appsettings.json               # Production configuration
├── appsettings.Development.json   # Development configuration
├── Program.cs                     # Application entry point
└── MabelApi.csproj               # Project file
```

## Development

### OpenAPI/Swagger

When running in development mode, the OpenAPI endpoint is available at:
- `/openapi/v1.json`

You can use tools like [Swagger UI](https://swagger.io/tools/swagger-ui/) or [Postman](https://www.postman.com/) to interact with the API.

### Adding New Features

1. **Create a Model**: Add a new class in `Models/` that extends `BaseModel` from Supabase
2. **Create a Controller**: Add a new controller in `Controllers/` with API endpoints
3. **Update Database**: Create corresponding tables in Supabase

### Environment Variables

For production, consider using environment variables instead of hardcoding credentials:

```bash
export Supabase__Url="https://your-project.supabase.co"
export Supabase__Key="your-anon-key"
```

## Security Considerations

- ⚠️ Never commit your Supabase credentials to version control
- ⚠️ Use environment variables or secure configuration management in production
- ⚠️ Configure proper Row Level Security (RLS) policies in Supabase
- ⚠️ Use service role key only on the server side, never expose it to clients

## NuGet Packages

- `Supabase` (v1.1.1) - Official Supabase client for .NET

## Troubleshooting

### Connection Issues

If you encounter connection issues:
1. Verify your Supabase URL and key are correct
2. Check that your Supabase project is active
3. Ensure your network allows connections to Supabase

### Build Errors

If you encounter build errors:
```bash
dotnet clean
dotnet restore
dotnet build
```

## Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [Supabase Documentation](https://supabase.com/docs)
- [Supabase .NET Client](https://github.com/supabase-community/supabase-csharp)

## License

This project is provided as-is for educational and development purposes.

# mabel-api
