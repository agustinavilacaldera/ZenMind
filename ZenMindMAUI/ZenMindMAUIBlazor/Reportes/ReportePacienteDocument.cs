using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZenMindMAUIBlazor.Controllers;
using ZenMindMAUIBlazor.Models.Data;
using Colors = QuestPDF.Helpers.Colors;


namespace ZenMindMAUIBlazor.Reportes;

internal class ReportePacienteDocument : IDocument
{
  private ModelController model;
  private Medicos medico;
  private Pacientes paciente;
  public ReportePacienteDocument(ModelController model)
  {
    this.model = model;
  }
  public ReportePacienteDocument SetPaciente(int pId)
  {
    paciente=model.CargarPaciente(pId);
    return this;
  }
  public ReportePacienteDocument SedMedico(int mId)
  {
    medico=model.CargarMedico(mId);
    return this;
  }
  public void Compose(IDocumentContainer container)
  {
    container.Page(page =>
    {
      //Características de la página
      page.Size(PageSizes.Letter);
      
      page.Margin(2, Unit.Centimetre);
      //Titulo
      page.Header().Text("ZenMind").Bold().FontSize(20).AlignCenter();

      //Datos del paciente
      page.Content().Column(column =>
      {
        
        column.Item().Table(table =>
        {
          table.ColumnsDefinition(column =>
          {
            column.ConstantColumn(100);
            column.ConstantColumn(150);
          });
          table.Cell().Text("ID:");
          table.Cell().Text(paciente.Id.ToString());
          table.Cell().Text("NOMBRES:");
          table.Cell().Text(paciente.SurName.ToUpper()+" "+paciente.Name.ToUpper());
          table.Cell().Text("EDAD:");
          table.Cell().Text(paciente.Birthday.ToShortDateString());
          table.Cell().Text("CALIFICACION:");
          table.Cell().Text(paciente.ObtenerCalificacion().ToString());
        });
        column.Spacing(20);
        column.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Black);
        //Lista de Asignaciones de test
        List<TestAssignments> testAssignmentsList = model.ListarTestAssignmentRespondidosPorPaciente(paciente.Id);
        column.Item().Table(table =>
        {
          table.ColumnsDefinition(column =>
          {
            column.ConstantColumn(25);
            column.ConstantColumn(70);
            column.ConstantColumn(230);
            column.ConstantColumn(80);
          });
          table.Cell().Text("ID");
          table.Cell().Text("FECHA");
          table.Cell().Text("TITULO");
          table.Cell().Text("PUNTUACION");
          column.Item().PaddingVertical(5).LineHorizontal(1).LineColor(Colors.Black);
          foreach (TestAssignments ta in testAssignmentsList)
          {
            table.Cell().Text(ta.Id.ToString()).AlignCenter();
            table.Cell().Text(ta.Date.ToShortDateString()).AlignLeft();
            table.Cell().Text(model.CargarTest(ta.TestId).Title);
            table.Cell().Text(model.CargarTestAssignment(ta.Id).ObtenerCalificacion().ToString()).AlignRight();
          }
        });
      });
      
      //page.Content().Column(column =>
      //{
      //  column.Item().Table(table =>
      //  {
      //    table.ColumnsDefinition(column =>
      //    {
      //      column.ConstantColumn(100);
      //      column.ConstantColumn(150);
      //    });
      //  });
      //});
    });
  }
}
