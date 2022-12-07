using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        public string CompanyName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserToken { get; set; }
    }


    public enum Roles
    {
        Vendor,
        Customer
    }
}
