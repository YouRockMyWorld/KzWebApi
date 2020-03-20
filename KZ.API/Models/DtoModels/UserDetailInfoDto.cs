using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.DtoModels
{
    public class UserDetailInfoDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } = "";
        public string PersonName { get; set; } = "";
        public string CompanyName { get; set; } = "";
    }
}
