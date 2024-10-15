using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models.Data
{
  internal class ResultadoTest
  {
    private List<TestFillOuts> testFillOuts;
    private List<FilaResultadoTest> hojaResultadoTest;

    public ResultadoTest(List<TestFillOuts> testFillOuts)
    {
      this.testFillOuts = testFillOuts;
      preparaHojaDatos();
    }
    private void preparaHojaDatos()
    {
      hojaResultadoTest = new();
      for (int i = 1; i <= 5; i++)
      {
        hojaResultadoTest.Add(new()
        {
          cantidad = i,
          valor = testFillOuts.Where(x => x.Answer == i).Count()
        });
      }
    }
    private int sumaProducto()
    {
      int sum = 0;
      foreach (FilaResultadoTest f in hojaResultadoTest)
      {
        sum += f.Producto();
      }
      return sum;
    }
    public float ObtenerPonderado()
    {
      try { return sumaProducto() / testFillOuts.Count; }
      catch { return 0; }

    }

  }
}
