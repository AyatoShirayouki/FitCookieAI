using FitCookieAI_ApplicationService.DTOs.UserRelated;

namespace AdminPanel.Models.Users
{
    public class UserDetailsVM
    {
        public UserDTO User { get; set; }
        public string? Error { get; set; }
    }
}
