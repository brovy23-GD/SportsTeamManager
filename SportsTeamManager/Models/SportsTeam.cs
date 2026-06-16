using System.ComponentModel.DataAnnotations;

namespace SportsTeamManager.Models;

public class SportsTeam
{
    [Key]
    public int TeamId { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Sport { get; set; }
    public int FoundedYear { get; set; }
    public int ChampionshipsWon { get; set; }
}
