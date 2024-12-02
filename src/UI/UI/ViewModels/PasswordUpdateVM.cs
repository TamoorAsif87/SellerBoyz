using System.ComponentModel.DataAnnotations;

namespace UI.ViewModels
{
    public class PasswordUpdateVM
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string newPassword { get; set; }
    }
}
