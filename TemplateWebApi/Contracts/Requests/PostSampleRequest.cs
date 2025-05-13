namespace TemplateWebApi.Contracts.Requests;

public record struct PostSampleRequest(
    string Data1,
    string? Data2
    );