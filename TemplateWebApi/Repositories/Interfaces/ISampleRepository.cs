namespace TemplateWebApi.Repositories.Interfaces;

public interface ISampleRepository
{
    Task<bool> GetSample(int id, CancellationToken cancellationToken);
}