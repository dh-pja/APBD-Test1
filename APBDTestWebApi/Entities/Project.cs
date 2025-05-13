namespace APBDTestWebApi.Entities;

public class Project
{
    public int IdProject { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Deadline { get; set; }
    
    public ICollection<TaskEntity>? Tasks { get; set; }
}