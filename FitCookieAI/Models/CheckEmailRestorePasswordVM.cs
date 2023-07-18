using System.ComponentModel.DataAnnotations;

namespace FitCookieAI.Models
{
    public class CheckEmailRestorePasswordVM
    {
        [Required(ErrorMessage = "This field is required!")]
        public string? Email { get; set; }  
    }
}
