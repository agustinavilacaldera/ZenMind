using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models.Data
{
    internal class MedicoPaciente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } = -1;
        [Indexed]
        public int MedicosId { get; set; }
        [Indexed]
        public int PacientesId { get; set; }
    }
}
