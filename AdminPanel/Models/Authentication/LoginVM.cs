using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.Authentication
{
    public class LoginVM
    {
        [Required(ErrorMessage = "This field is required!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? Password { get; set; }

        public string Error { get; set; }
        public string Message { get; set; }
    }
}
