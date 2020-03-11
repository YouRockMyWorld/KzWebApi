using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.DtoModels
{
    public class OperationLogDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Module { get; set; }
        public string LogType { get; set; }
        public DateTime OperationTime { get; set; }
        public string IPAddress { get; set; }
        public string Details { get; set; }
    }
}
