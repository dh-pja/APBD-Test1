using APBDTestWebApi.Entities;

namespace APBDTestWebApi.Repositories.Interfaces;

public interface ITeamMemberRepository
{
    // Task<bool> GetSample(int id, CancellationToken cancellationToken);
    Task<TeamMember?> GetTeamMember(int id, CancellationToken cancellationToken);
}