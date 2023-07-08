using FitCookieAI_ApplicationService.DTOs.AdminRelated;

namespace AdminPanel.Models.Admins
{
    public class AdminsIndexVM
    {
        public List<AdminDTO>? Admins { get; set; }
        public List<AdminStatusDTO>? AdminStatuses { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
