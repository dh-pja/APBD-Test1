using TemplateWebApi.Repositories.Interfaces;

namespace TemplateWebApi.Repositories;

public class SampleRepository(IConfiguration configuration) : ISampleRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("Default") ??
                                                throw new ArgumentException("Connection string not found");

    public Task<bool> GetSample(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}