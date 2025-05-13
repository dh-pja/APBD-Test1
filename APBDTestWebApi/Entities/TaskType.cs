namespace APBDTestWebApi.Entities;

public class TaskType
{
    public int IdTaskType { get; set; }
    public string Name { get; set; } = null!;
    
    public ICollection<TaskEntity>? Tasks { get; set; }
}