using APBDTestWebApi.Contracts.Responses;
using APBDTestWebApi.Entities;

namespace APBDTestWebApi.Mappers;

public static class TaskMapper
{
    public static ICollection<GetTaskResponse> MapToSample(this ICollection<TaskEntity> samples)
    {
        List<GetTaskResponse> tasks = new List<GetTaskResponse>();
        foreach (var task in samples)
        {
            tasks.Add(new GetTaskResponse()
            {
                Name = task.Name,
                Description = task.Description,
                Deadline = task.Deadline,
                ProjectName = task.Project!.Name,
                TaskType = task.TaskType!.Name,
            });
        }

        return tasks;
    }
}