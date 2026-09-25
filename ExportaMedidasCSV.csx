using System.Text;

string carpetaDestino = @"C:\Backups_PowerBI\";
if (!System.IO.Directory.Exists(carpetaDestino))
    System.IO.Directory.CreateDirectory(carpetaDestino);

string ruta = System.IO.Path.Combine(carpetaDestino, $"Medidas_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

var sb = new StringBuilder();
sb.AppendLine("Tabla;Medida;Expresion;CarpetaVisualizacion;Descripcion;FormatoCadena;Oculta");

foreach (var m in Model.AllMeasures.OrderBy(x => x.Table.Name).ThenBy(x => x.Name))
{
    string expresion = (m.Expression ?? "").Replace("\r", " ").Replace("\n", " ").Replace(";", ",");
    string descripcion = (m.Description ?? "").Replace(";", ",");
    sb.AppendLine($"{m.Table.Name};{m.Name};{expresion};{m.DisplayFolder};{descripcion};{m.FormatString};{m.IsHidden}");
}

System.IO.File.WriteAllText(ruta, sb.ToString(), Encoding.UTF8);

Info($"Se han exportado {Model.AllMeasures.Count()} medidas a:\n{ruta}");
