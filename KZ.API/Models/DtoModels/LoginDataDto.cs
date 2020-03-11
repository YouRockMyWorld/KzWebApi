using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.DtoModels
{
    public class LoginDataDto
    {
        [Required(ErrorMessage ="{0}字段不能为空")]
        public string Username { get; set; }
        [Required(ErrorMessage = "{0}字段不能为空")]
        public string Password { get; set; }
    }
}
