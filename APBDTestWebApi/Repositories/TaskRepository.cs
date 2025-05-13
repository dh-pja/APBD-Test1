using APBDTestWebApi.Entities;
using APBDTestWebApi.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace APBDTestWebApi.Repositories;

public class TaskRepository(IConfiguration configuration) : ITaskRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("Default") ??
                                                throw new ArgumentException("No connection string configured.");


    public async Task<ICollection<TaskEntity>?> GetTasksAssignedTo(int memberId, CancellationToken cancellationToken)
    {
        const string query = """
                              SELECT
                                  t.IdTask, t.Name, t.Description, t.Deadline,
                                  p.IdProject, p.Name, p.Deadline,
                                  TT.IdTaskType, TT.Name
                                  FROM Task as t 
                                  JOIN Project p on p.IdProject = t.IdProject
                                  JOIN dbo.TaskType TT on TT.IdTaskType = t.IdTaskType
                                  WHERE t.IdAssignedTo = @IdAssignedTo
                                  ORDER BY t.Deadline DESC;
                             """;
        
        List<TaskEntity> taskEntities = new List<TaskEntity>();

        await using var con = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(query, con);
        
        cmd.Parameters.AddWithValue("@IdAssignedTo", memberId);
        
        var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            TaskEntity taskEntity = new TaskEntity()
            {
                IdTask = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Deadline = reader.GetDateTime(3),
                Project = new Project()
                {
                    IdProject = reader.GetInt32(4),
                    Name = reader.GetString(5),
                    Deadline = reader.GetDateTime(6),
                },
                TaskType = new TaskType()
                {
                    IdTaskType = reader.GetInt32(7),
                    Name = reader.GetString(8),
                }
            };
            
            taskEntities.Add(taskEntity);
        }
        
        await reader.CloseAsync();

        return taskEntities;
    }

    public async Task<ICollection<TaskEntity>?> GetTasksCreatedBy(int memberId, CancellationToken cancellationToken)
    {
        const string query = """
                              SELECT
                                  t.IdTask, t.Name, t.Description, t.Deadline,
                                 p.IdProject, p.Name, p.Deadline,
                                 TT.IdTaskType, TT.Name
                                 FROM Task as t 
                                 JOIN Project p on p.IdProject = t.IdProject
                                 JOIN dbo.TaskType TT on TT.IdTaskType = t.IdTaskType
                                  ORDER BY t.Deadline DESC;
                             """;
        
        List<TaskEntity> taskEntities = new List<TaskEntity>();

        await using var con = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(query, con);
        
        cmd.Parameters.AddWithValue("@IdCreator", memberId);
        
        var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            TaskEntity taskEntity = new TaskEntity()
            {
                IdTask = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Deadline = reader.GetDateTime(3),
                Project = new Project()
                {
                    IdProject = reader.GetInt32(4),
                    Name = reader.GetString(5),
                    Deadline = reader.GetDateTime(6),
                },
                TaskType = new TaskType()
                {
                    IdTaskType = reader.GetInt32(7),
                    Name = reader.GetString(8),
                }
            };
            
            taskEntities.Add(taskEntity);
        }
        
        await reader.CloseAsync();

        return taskEntities;
    }

    public async Task<(bool success, string message)> DeleteDataAboutProject(int projectId, CancellationToken cancellationToken)
    {
        await using SqlConnection con = new SqlConnection(_connectionString);
        await using var transaction = await con.BeginTransactionAsync(cancellationToken);

        try
        {
            const string deleteTasksQuery = """
                                            DELETE FROM Task where IdProject = @IdProject;
                                            """;
            const string deleteProjectQuery = """
                                              DELETE FROM Project where IdProject = @IdProject;
                                              """;

            await using (SqlCommand cmd = new SqlCommand(deleteTasksQuery, con, (SqlTransaction)transaction))
            {
                cmd.Parameters.AddWithValue("@IdProject", projectId);
                
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }

            await using (SqlCommand cmd = new SqlCommand(deleteProjectQuery, con, (SqlTransaction)transaction))
            {
                cmd.Parameters.AddWithValue("@IdProject", projectId);
                
                await cmd.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
            return (true, string.Empty);
        } catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return (false, "Unknown Error");
        }
    }
}