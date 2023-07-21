using System.ComponentModel.DataAnnotations;

namespace FitCookieAI.Models
{
    public class CheckCodeRestorePasswordVM
    {
        [Required(ErrorMessage = "This field is required!")]
        public string? Code { get; set; }
    }
}
