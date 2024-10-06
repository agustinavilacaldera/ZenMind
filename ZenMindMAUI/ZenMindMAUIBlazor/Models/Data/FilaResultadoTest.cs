using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMindMAUIBlazor.Models.Data
{
    internal class FilaResultadoTest
    {
        public int valor;
        public int cantidad;
        public Func<int> Producto => () => valor * cantidad;
    }
}
