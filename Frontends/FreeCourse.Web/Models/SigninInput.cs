using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name ="E-poçtunuz")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Şifrə")]
        public string Password { get; set; }

        [Display(Name = "Şifrəni xatırla")]
        public bool Remember { get; set; }
    }
}
