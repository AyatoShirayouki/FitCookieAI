using FitCookieAI_ApplicationService.DTOs.AdminRelated;
using FitCookieAI_ApplicationService.DTOs.UserRelated;

namespace AdminPanel.Models.Home
{
    public class HomeIndexVM
    {
        public string? Error { get; set; }
        public List<UserDTO> Users { get; set; }
        public List<AdminDTO> Admins { get; set; }
        public List<AdminStatusDTO> AdminStatuses { get; set; }
        public List<RefreshAdminTokenDTO> RefreshAdminTokens { get; set; }
        public List<RefreshUserTokenDTO> RefreshUserTokens { get; set; }
    }
}
