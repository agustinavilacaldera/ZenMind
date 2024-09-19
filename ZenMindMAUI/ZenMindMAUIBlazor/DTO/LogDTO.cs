using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMind.DTO
{
    public class LogDTO
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }
    }
}
