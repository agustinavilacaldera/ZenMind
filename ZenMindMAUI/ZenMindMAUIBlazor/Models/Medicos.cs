using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models
{
  internal class Medicos
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string SurName {  get; set; }
    [Unique]
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Especiality {  get; set; }
    public DateTime Birthday { get; set; }
    [Indexed]
    public string UsersId { get; set; }
  }
}
