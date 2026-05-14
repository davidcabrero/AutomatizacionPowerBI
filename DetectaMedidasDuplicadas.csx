// Diccionario para almacenar expresiones y medidas asociadas
var expresiones = new Dictionary<string, List<Measure>>();

// Recorremos todas las medidas del modelo
foreach (var tabla in Model.Tables)
{
    foreach (var medida in tabla.Measures)
    {
        var expr = medida.Expression.Trim();

        if (!expresiones.ContainsKey(expr))
        {
            expresiones[expr] = new List<Measure>();
        }

        expresiones[expr].Add(medida);
    }
}

// Mostrar resultados
int duplicadas = 0;

foreach (var grupo in expresiones)
{
    if (grupo.Value.Count > 1)
    {
        duplicadas++;
        Output.WriteLine("⚠️ Medidas duplicadas encontradas:");

        foreach (var m in grupo.Value)
        {
            Output.WriteLine($" - {m.DaxObjectFullName}");
            
            // OPCIONAL: Marcar descripción
            m.Description = "⚠️ Posible medida duplicada";
        }

        Output.WriteLine("-----------------------------------");
    }
}

Output.WriteLine($"Total grupos de medidas duplicadas: {duplicadas}");
