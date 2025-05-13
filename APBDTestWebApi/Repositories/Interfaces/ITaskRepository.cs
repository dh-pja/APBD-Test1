using APBDTestWebApi.Entities;

namespace APBDTestWebApi.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<ICollection<TaskEntity>?> GetTasksAssignedTo(int memberId, CancellationToken cancellationToken);
    
    Task<ICollection<TaskEntity>?> GetTasksCreatedBy(int memberId, CancellationToken cancellationToken);
    
    Task<(bool success, string message)> DeleteDataAboutProject(int projectId, CancellationToken cancellationToken);
}