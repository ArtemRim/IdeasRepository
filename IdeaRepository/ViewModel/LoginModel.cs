using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdeaRepository.ViewModel
{
    public class LoginModel
    {
        [Required]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "Username length must be between 4 to 32 characters")]
        public string Login { get; set; }

        [Required]   
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password length must be between 6 to 32 characters")]
        public string Password { get; set; }
    }
}