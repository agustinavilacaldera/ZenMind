using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenMindMAUIBlazor.Models.Data;

namespace ZenMindMAUIBlazor.Models.Report
{
  internal class ReportePaciente
  {
    public Pacientes Pacientes { get; set; }
    public List<TestAssignments> TestAssignments { get; set; }
  }
}
