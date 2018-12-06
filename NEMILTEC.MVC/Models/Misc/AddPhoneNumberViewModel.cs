using System.ComponentModel.DataAnnotations;

namespace NEMILTEC.MVC.Models.Misc
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}