namespace APBDTestWebApi.Entities;

public class TeamMember
{
    public int IdTeamMember { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}