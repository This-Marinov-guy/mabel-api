# Supabase Setup Guide

This guide will help you set up Supabase for the Mabel API project.

## Step 1: Create a Supabase Project

1. Go to [https://supabase.com](https://supabase.com)
2. Sign up or log in to your account
3. Click "New Project"
4. Fill in the project details:
   - **Name**: Choose a name (e.g., "mabel-api")
   - **Database Password**: Create a strong password (save this!)
   - **Region**: Choose the closest region to your users
   - **Pricing Plan**: Free tier is sufficient for development

## Step 2: Get Your API Credentials

1. Once your project is created, go to **Settings** → **API**
2. You'll need two values:
   - **Project URL**: `https://xxxxxxxxxxxxx.supabase.co`
   - **anon/public key**: A long string starting with `eyJ...`

3. Update your `appsettings.Development.json`:

```json
{
  "Supabase": {
    "Url": "https://xxxxxxxxxxxxx.supabase.co",
    "Key": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

## Step 3: Create the Database Schema

1. In your Supabase dashboard, go to **SQL Editor**
2. Click "New Query"
3. Paste and run the following SQL:

```sql
-- Create todos table
CREATE TABLE todos (
  id SERIAL PRIMARY KEY,
  title TEXT NOT NULL,
  description TEXT,
  is_completed BOOLEAN DEFAULT FALSE,
  created_at TIMESTAMPTZ DEFAULT NOW(),
  updated_at TIMESTAMPTZ DEFAULT NOW()
);

-- Create an index on created_at for better query performance
CREATE INDEX idx_todos_created_at ON todos(created_at DESC);

-- Enable Row Level Security
ALTER TABLE todos ENABLE ROW LEVEL SECURITY;

-- Create a policy to allow all operations (for development)
-- In production, you should create more restrictive policies
CREATE POLICY "Allow all operations for development" ON todos
  FOR ALL
  USING (true)
  WITH CHECK (true);

-- Create a function to automatically update updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Create a trigger to call the function before updates
CREATE TRIGGER update_todos_updated_at BEFORE UPDATE ON todos
FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
```

## Step 4: (Optional) Add Sample Data

If you want to test with some sample data:

```sql
INSERT INTO todos (title, description, is_completed) VALUES
  ('Setup Supabase', 'Configure Supabase for the API', true),
  ('Create API endpoints', 'Build RESTful endpoints for todos', true),
  ('Test the API', 'Test all CRUD operations', false),
  ('Deploy to production', 'Deploy the API to a hosting service', false);
```

## Step 5: Configure Row Level Security (RLS) for Production

For production, you should implement proper RLS policies. Here are some examples:

### Example: User-specific todos (if you add authentication)

```sql
-- First, add a user_id column
ALTER TABLE todos ADD COLUMN user_id UUID REFERENCES auth.users(id);

-- Drop the development policy
DROP POLICY "Allow all operations for development" ON todos;

-- Create user-specific policies
CREATE POLICY "Users can view their own todos" ON todos
  FOR SELECT
  USING (auth.uid() = user_id);

CREATE POLICY "Users can create their own todos" ON todos
  FOR INSERT
  WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can update their own todos" ON todos
  FOR UPDATE
  USING (auth.uid() = user_id)
  WITH CHECK (auth.uid() = user_id);

CREATE POLICY "Users can delete their own todos" ON todos
  FOR DELETE
  USING (auth.uid() = user_id);
```

## Step 6: Test the Connection

1. Run your .NET API:
   ```bash
   cd MabelApi
   dotnet run
   ```

2. Test the connection by making a GET request:
   ```bash
   curl https://localhost:7XXX/api/todo
   ```

   You should see an empty array `[]` or your sample data if you added any.

## Troubleshooting

### Error: "Invalid API key"
- Double-check that you copied the correct anon/public key from Supabase
- Make sure there are no extra spaces or line breaks in the key

### Error: "relation 'todos' does not exist"
- Make sure you ran the SQL to create the todos table
- Check that you're connected to the correct Supabase project

### Error: "permission denied for table todos"
- Check that Row Level Security policies are set up correctly
- For development, you can temporarily disable RLS: `ALTER TABLE todos DISABLE ROW LEVEL SECURITY;`

### Connection timeout
- Check your internet connection
- Verify the Supabase URL is correct
- Check if your firewall is blocking the connection

## Additional Resources

- [Supabase Documentation](https://supabase.com/docs)
- [Supabase Row Level Security](https://supabase.com/docs/guides/auth/row-level-security)
- [Supabase C# Client](https://github.com/supabase-community/supabase-csharp)

## Security Best Practices

1. **Never commit credentials**: Keep your Supabase URL and key out of version control
2. **Use environment variables**: In production, use environment variables or secure vaults
3. **Enable RLS**: Always enable Row Level Security in production
4. **Use service role key carefully**: The service role key bypasses RLS - only use it server-side
5. **Rotate keys**: Regularly rotate your API keys
6. **Monitor usage**: Keep an eye on your Supabase dashboard for unusual activity

## Next Steps

Once your Supabase is set up:

1. ✅ Test all CRUD operations using the API
2. ✅ Implement authentication if needed
3. ✅ Add more tables and relationships
4. ✅ Set up proper RLS policies
5. ✅ Configure backups and monitoring

