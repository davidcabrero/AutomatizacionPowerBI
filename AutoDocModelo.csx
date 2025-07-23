using System.Text;
using System.IO;

string outputPath = @"C:\Doc\DocumentacionModelo.md"; // Cambia si lo necesitas
var sb = new StringBuilder();

sb.AppendLine("# Documentación del Modelo Power BI");
sb.AppendLine("Generado: " + DateTime.Now.ToString());
sb.AppendLine();

sb.AppendLine("## Tablas y Columnas");

foreach (var table in Model.Tables.Where(t => !t.IsHidden))
{
    sb.AppendLine("### Tabla: " + table.Name);
    if (!string.IsNullOrEmpty(table.Description))
        sb.AppendLine("> " + table.Description);

    sb.AppendLine("| Columna | Tipo de datos | Es clave | Descripción |");
    sb.AppendLine("|---------|----------------|----------|-------------|");

    foreach (var column in table.Columns)
    {
        sb.AppendLine(string.Format("| {0} | {1} | {2} | {3} |",
            column.Name,
            column.DataType.ToString(),
            column.IsKey ? "Sí" : "",
            column.Description ?? ""));
    }

    sb.AppendLine();

    if (table.Measures.Count > 0)
    {
        sb.AppendLine("#### Medidas:");
        foreach (var measure in table.Measures)
        {
            sb.AppendLine("- " + measure.Name + ": `" + measure.Expression + "`");
            if (!string.IsNullOrEmpty(measure.Description))
                sb.AppendLine("> " + measure.Description);
        }
        sb.AppendLine();
    }

    sb.AppendLine(); // espacio extra entre tablas
}

sb.AppendLine("## Relaciones");
sb.AppendLine("| Origen | Columna | Destino | Columna | Activa |");
sb.AppendLine("|--------|---------|---------|---------|--------|");

foreach (var rel in Model.Relationships)
{
    sb.AppendLine(string.Format("| {0} | {1} | {2} | {3} | {4} |",
        rel.FromTable.Name,
        rel.FromColumn.Name,
        rel.ToTable.Name,
        rel.ToColumn.Name,
        rel.IsActive ? "Sí" : "No"));
}

File.WriteAllText(outputPath, sb.ToString());
Info("✅ Documentación generada en: " + outputPath);