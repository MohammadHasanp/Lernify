namespace Common.Domain;

public class Entity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsDelete { get; set; }

    public Entity()
    {
        this.Id = Guid.NewGuid();
        this.CreationDate = DateTimeOffset.UtcNow;
        this.IsDelete = false;
    }
}
