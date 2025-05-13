namespace APBDTestWebApi.Entities;

public class TaskEntity
{
    public int IdTask { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Deadline { get; set; }
    
    public Project? Project { get; set; }
    public TaskType? TaskType { get; set; }
    public TeamMember? AssignedTo { get; set; }
    public TeamMember? CreatedBy { get; set; }
}