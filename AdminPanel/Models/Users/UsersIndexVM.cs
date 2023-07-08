using FitCookieAI_ApplicationService.DTOs.UserRelated;

namespace AdminPanel.Models.Users
{
    public class UsersIndexVM
    {
        public List<UserDTO>? Users { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
