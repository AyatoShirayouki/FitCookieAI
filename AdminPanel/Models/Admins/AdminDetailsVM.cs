using FitCookieAI_ApplicationService.DTOs.AdminRelated;

namespace AdminPanel.Models.Admins
{
    public class AdminDetailsVM
    {
        public AdminDTO? Admin { get; set; }
        public AdminStatusDTO? AdminStatus { get; set; }
        public string? Error { get; set; }
    }
}
