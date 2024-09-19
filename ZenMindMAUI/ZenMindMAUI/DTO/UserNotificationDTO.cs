using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMind.DTO
{
    public class UserNotificationDTO
    {
        public int UserId { get; set; }
        public int? UserType { get; set; }
        public string Name { get; set; } = "";       
        public string Email { get; set; } = "";
        public string Telephone { get; set; } = "";
        public int TestId { get; set; }
    }
}
