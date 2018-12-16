using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movie_Rent.Models
{
    public class User
    {// class that represent the normal user and the meneger 
        [Key]
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(10,MinimumLength =4, ErrorMessage = "Password must be between 4 and 10 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mail address is required")]
        public string Mail { get; set; }

        public User() {}

        public User(string Type = null, string UserName = null, string Password = null, string Mail = null)
        {
            this.Type = Type;
            this.UserName = UserName;
            this.Password = Password;
            this.Mail = Mail;
        }

    }
}