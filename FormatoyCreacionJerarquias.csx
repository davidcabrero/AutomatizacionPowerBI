foreach (var medida in Model.AllMeasures)
{
    var nombre = medida.Name.ToLower();
    var expr = medida.Expression.ToLower();

    // ---- Porcentaje ----
    if (nombre.Contains("%") || nombre.Contains("porcentaje") || expr.Contains("/"))
    {
        medida.FormatString = "0.00%";
    }

    // ---- Moneda ----
    else if (nombre.Contains("importe") || nombre.Contains("total") || 
             nombre.Contains("precio") || nombre.Contains("coste") ||
             nombre.Contains("monto") || nombre.Contains("€") || expr.Contains("sum("))
    {
        medida.FormatString = "#,0.00 €";
    }

    // ---- Tiempo / Duración ----
    else if (nombre.Contains("tiempo") || nombre.Contains("hora") || 
             nombre.Contains("duracion") || expr.Contains("datediff"))
    {
        medida.FormatString = "hh:mm:ss";
    }

    // ---- Conteos ----
    else if (nombre.Contains("conteo") || nombre.Contains("count") || expr.Contains("count("))
    {
        medida.FormatString = "#,0";
    }

    // ---- Decimales generales ----
    else
    {
        medida.FormatString = "#,0.00";
    }
}

Info("✅ Formatos aplicados a todas las medidas automáticamente.");

foreach (var tabla in Model.Tables.Where(t => t.Name.ToLower().Contains("fecha") || t.Name.ToLower().Contains("calendar")))
{
    if (tabla.Hierarchies.Count == 0)
    {
        var jerarquia = tabla.AddHierarchy("Fecha Jerarquía");
        var year = tabla.Columns.FirstOrDefault(c => c.Name.ToLower().Contains("año") || c.Name.ToLower().Contains("year"));
        var quarter = tabla.Columns.FirstOrDefault(c => c.Name.ToLower().Contains("trimestre") || c.Name.ToLower().Contains("quarter"));
        var month = tabla.Columns.FirstOrDefault(c => c.Name.ToLower().Contains("mes") || c.Name.ToLower().Contains("month"));
        var day = tabla.Columns.FirstOrDefault(c => c.Name.ToLower().Contains("día") || c.Name.ToLower().Contains("day"));

        if (year != null) jerarquia.AddLevel(year);
        if (quarter != null) jerarquia.AddLevel(quarter);
        if (month != null) jerarquia.AddLevel(month);
        if (day != null) jerarquia.AddLevel(day);
    }
}
Info("✅ Jerarquías de fechas generadas.");