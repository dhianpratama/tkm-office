using System.ComponentModel.DataAnnotations;

namespace TKM_Office_API.RequestParam
{
    public class UserRegistration
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class UserResetPassword : UserRegistration
    {
        
    }

    public class UserChangePassword : UserRegistration
    {
        public string OldPassword { get; set; }
    }
}