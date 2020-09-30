using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Class_Room.Models
{
    public class Login
    {
        public int userId { get; set; }
        [Display(Name = "UserName")]
        [Required(AllowEmptyStrings =false, ErrorMessage =" UserName is required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string type { get; set; }
    }
}