using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models.Data
{
    internal class Questions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Statement { get; set; }
        [Indexed]
        public int TestsId { get; set; }
    }
}
