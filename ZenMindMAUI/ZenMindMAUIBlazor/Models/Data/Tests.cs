using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models.Data
{
    internal class Tests
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = -1;
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        [Ignore]
        public List<Questions> Questions { get; set; }
    }
}
