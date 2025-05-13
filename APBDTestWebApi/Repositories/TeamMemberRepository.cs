using APBDTestWebApi.Entities;
using APBDTestWebApi.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace APBDTestWebApi.Repositories;

public class TeamMemberRepository(IConfiguration configuration) : ITeamMemberRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("Default") ??
                                                throw new ArgumentException("Connection string not found");


    public async Task<TeamMember?> GetTeamMember(int id, CancellationToken cancellationToken)
    {
        const string query = "SELECT IdTeamMember, FirstName, LastName, Email FROM TeamMembers WHERE IdTeamMember = @Id";
        
        await using var connection = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(query, connection);
        
        cmd.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync(cancellationToken);

        var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        
        TeamMember? teamMember = null;
        
        while (await reader.ReadAsync(cancellationToken))
        {
            teamMember = new TeamMember()
            {
                IdTeamMember = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3)
            };

        }

        await reader.CloseAsync();
        
        return teamMember;
    }
}