namespace APBDTestWebApi.Contracts.Responses;

public record struct GetTaskResponse(
    string Name,
    string Description,
    DateTime Deadline,
    string ProjectName,
    string TaskType
    );