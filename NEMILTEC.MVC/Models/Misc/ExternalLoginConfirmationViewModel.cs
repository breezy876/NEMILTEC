using System.ComponentModel.DataAnnotations;

namespace NEMILTEC.MVC.Models.Misc
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
