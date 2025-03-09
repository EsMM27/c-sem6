using System.ComponentModel.DataAnnotations;

namespace RP1_L00148202.Pages.PageViewModels
{
    public class Login
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
