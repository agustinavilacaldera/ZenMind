using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models
{
  internal class TestFillOuts
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public int TestId { get; set; }
    [Indexed]
    public int QuestionsId { get; set; }
    public int Answer { get; set; }
  }
}
