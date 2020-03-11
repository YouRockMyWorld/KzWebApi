using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.Others
{
    public class OperationLogResult
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public byte Module { get; set; }
        public byte LogType { get; set; }
        public DateTime OperationTime { get; set; }
        public long IPAddress { get; set; }
        public string Details { get; set; }
    }
}
