using Domain.Enums;

namespace Presentation.Entities.View;

public class UserStatusForView
{
    public int Score { get; set; }
    public int Submissions { get; set; }
    public int PerfectSubmissions { get; set; }
    public string Rank { get; set; } = Ranks.Novice.ToString();

}
