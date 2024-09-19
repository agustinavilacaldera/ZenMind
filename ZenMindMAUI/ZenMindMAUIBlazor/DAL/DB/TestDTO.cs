using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZenMind.DAL.DB
{
  public class TestDTO
  {
    [PrimaryKey, AutoIncrement]
    public int TestId { get; set; }
    public string Title { get; set; }
    public string Date { get; set; }
    public string Description { get; set; }
    public bool TestTaken { get; set; }
    public int Score { get; set; }

  }
}
