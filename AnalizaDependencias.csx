// AnalizaDependencias.csx
// Genera un informe de dependencias: para cada medida, qué columnas y qué otras
// medidas utiliza en su expresión DAX. Complementa a DetectaMedidasDuplicadas.csx
// y DetectaNoUsoColumns.csx: aquí ves el "árbol" completo, no solo si se usa o no.

using System.Text;
using TabularEditor.TOMWrapper;

var sb = new StringBuilder();
sb.AppendLine("Medida;Tabla;DependeDe;NumDependencias");

foreach (var m in Model.AllMeasures.OrderBy(x => x.Table.Name).ThenBy(x => x.Name))
{
    // DependsOn recoge todos los objetos (columnas, otras medidas) referenciados
    // en la expresión DAX de la medida.
    var dependencias = m.DependsOn
                         .OfType<IDaxObject>()
                         .Select(d => d.DaxObjectFullName)
                         .Distinct()
                         .ToList();

    sb.AppendLine($"{m.Name};{m.Table.Name};{string.Join(" | ", dependencias)};{dependencias.Count}");
}

string carpetaDestino = @"C:\Backups_PowerBI\";
if (!System.IO.Directory.Exists(carpetaDestino))
    System.IO.Directory.CreateDirectory(carpetaDestino);

string ruta = System.IO.Path.Combine(carpetaDestino, $"Dependencias_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
System.IO.File.WriteAllText(ruta, sb.ToString());

Info($"Informe de dependencias generado en:\n{ruta}");

// Bonus: medidas con más de 10 dependencias suelen ser candidatas a refactorizar
// (demasiada lógica en una sola medida). Puedes filtrar el CSV por esa columna.
