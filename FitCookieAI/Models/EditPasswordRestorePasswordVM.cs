using System.ComponentModel.DataAnnotations;

namespace FitCookieAI.Models
{
    public class EditPasswordRestorePasswordVM
    {
        [Required(ErrorMessage = "This field is required!")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string? ConfirmPassword { get; set; }
    }
}
