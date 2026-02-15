using System.ComponentModel.DataAnnotations;

namespace FirmWebApp.Models
{
    public class LogInViewModel
    {
       
            public long Id { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string UserName { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public string ReturnUrl { get; set; }
      
    }
}
