using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace MabelApi.Models;

[Table("todos")]
public class TodoItem : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("is_completed")]
    public bool IsCompleted { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

