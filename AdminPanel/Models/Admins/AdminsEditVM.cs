using FitCookieAI_ApplicationService.DTOs.AdminRelated;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Admins
{
    public class AdminsEditVM
    {
        public List<AdminStatusDTO> AdminStatuses { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public int StatusId { get; set; }
        public string? NewPassword { get; set; }
        public string? Error { get; set; }
        public string? Message { get; set; }
        public string? ProfilePhotoName { get; set; }
        public IFormFile FileName { get; set; }
    }
}
