var columnasUsadas = new HashSet<string>();

// Relacionadas
foreach (var rel in Model.Relationships)
{
    columnasUsadas.Add(rel.FromTable.Name + "[" + rel.FromColumn.Name + "]");
    columnasUsadas.Add(rel.ToTable.Name + "[" + rel.ToColumn.Name + "]");
}

// Usadas en medidas
foreach (var medida in Model.AllMeasures)
{
    if (string.IsNullOrEmpty(medida.Expression)) continue;

    foreach (var tabla in Model.Tables)
    {
        foreach (var col in tabla.Columns)
        {
            var nombre = tabla.Name + "[" + col.Name + "]";
            if (medida.Expression.Contains(col.Name))
            {
                columnasUsadas.Add(nombre);
            }
        }
    }
}

var candidatas = new List<string>();

// Buscar columnas visibles no marcadas como usadas
foreach (var tabla in Model.Tables)
{
    foreach (var col in tabla.Columns)
    {
        var nombreCompleto = tabla.Name + "[" + col.Name + "]";

        if (!col.IsHidden && !columnasUsadas.Contains(nombreCompleto))
        {
            candidatas.Add(nombreCompleto);
        }
    }
}

// Mostrar resultado
if (candidatas.Count == 0)
{
    Info("✅ Todas las columnas visibles parecen estar en uso.");
}
else
{
    var mensaje = "🔍 Columnas visibles potencialmente no utilizadas:\n\n";
    foreach (var col in candidatas)
    {
        mensaje += "- " + col + "\n";
    }
    Error(mensaje);
}