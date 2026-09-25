string separador = " - "; // Cambia esto si usas otra convención, p.ej. "_" o "|"

int organizadas = 0;
foreach (var m in Model.AllMeasures)
{
    if (m.Name.Contains(separador))
    {
        string prefijo = m.Name.Split(new[] { separador }, StringSplitOptions.None)[0].Trim();
        if (m.DisplayFolder != prefijo)
        {
            m.DisplayFolder = prefijo;
            organizadas++;
        }
    }
    else if (string.IsNullOrEmpty(m.DisplayFolder))
    {
        m.DisplayFolder = "Sin clasificar";
        organizadas++;
    }
}

Info($"{organizadas} medidas reorganizadas en carpetas de visualización.");
