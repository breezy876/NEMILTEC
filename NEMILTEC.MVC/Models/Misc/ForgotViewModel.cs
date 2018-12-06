using System.ComponentModel.DataAnnotations;

namespace NEMILTEC.MVC.Models.Misc
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}