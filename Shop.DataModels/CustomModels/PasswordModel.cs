using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataModels.CustomModels
{
    public class PasswordModel
    {
        public int UserKey { get; set; }
        [Required(ErrorMessage ="Old password is required..")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm password is required")]
        public string ConfirmPassword { get; set; }

    }
}
