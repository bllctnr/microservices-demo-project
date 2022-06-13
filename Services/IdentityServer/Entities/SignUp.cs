using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Entities
{
    public class SignUp
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
    }
}
