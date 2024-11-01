﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models.Data
{
  internal class Pacientes
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } = -1;
    public string Name { get; set; }
    public string SurName { get; set; }
    [Unique]
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Birthday { get; set; }
    [Indexed]
    public string UsersId { get; set; }
    [Ignore]
    public List<TestFillOuts> TestFillOuts { get; set; }
    public float ObtenerCalificacion()
    {
      ResultadoTest rt = new(TestFillOuts);
      return rt.ObtenerPonderado();
    }
    public float ObtenerCalificacionUltimoTest()
    {
      try
      {
        DateTime d = TestFillOuts.Max(x => x.Fecha);
        List<TestFillOuts> lasttest = TestFillOuts.Where(x => x.Fecha == d).ToList();
        ResultadoTest rt = new(lasttest);
        return rt.ObtenerPonderado();
      }
      catch
      {
        return 1;
      }
      
    }
  }
}
