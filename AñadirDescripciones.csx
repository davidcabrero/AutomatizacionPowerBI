// ============================================================
// Tabular Editor 2 - Añadir descripciones a tablas, columnas y medidas
// ============================================================
// CONFIGURACIÓN
bool sobrescribir = false;      // true = reemplaza descripciones existentes
bool incluirDax = true;         // true = añade la expresión DAX a la descripción de las medidas
int maxLongitudDax = 250;       // máximo de caracteres de DAX a incluir

int nTablas = 0, nColumnas = 0, nMedidas = 0;

// Limpia saltos de línea y espacios múltiples, y recorta el texto
Func<string, int, string> Limpiar = (texto, max) =>
{
    if (string.IsNullOrWhiteSpace(texto)) return "";
    string s = System.Text.RegularExpressions.Regex.Replace(texto, @"\s+", " ").Trim();
    return s.Length > max ? s.Substring(0, max) + "..." : s;
};

foreach (var t in Model.Tables)
{
    // ---------- TABLA ----------
    if (sobrescribir || string.IsNullOrWhiteSpace(t.Description))
    {
        string tipoTabla = (t is CalculatedTable) ? "Tabla calculada" : "Tabla";
        string origen = "";
        if (t is CalculatedTable)
        {
            string dax = Limpiar(((CalculatedTable)t).Expression, maxLongitudDax);
            if (dax != "") origen = string.Format(" Definida mediante DAX: {0}", dax);
        }
        t.Description = string.Format("{0} '{1}'. Contiene {2} columna(s) y {3} medida(s).{4}",
            tipoTabla, t.Name, t.Columns.Count(c => c.Type != ColumnType.RowNumber), t.Measures.Count, origen);
        nTablas++;
    }

    // ---------- COLUMNAS ----------
    foreach (var c in t.Columns)
    {
        if (c.Type == ColumnType.RowNumber) continue; // columna interna, se omite

        if (sobrescribir || string.IsNullOrWhiteSpace(c.Description))
        {
            string desc;
            if (c.Type == ColumnType.Calculated)
            {
                string dax = Limpiar(((CalculatedColumn)c).Expression, maxLongitudDax);
                desc = string.Format("Columna calculada '{0}' de la tabla '{1}'. Tipo de dato: {2}.", c.Name, t.Name, c.DataType);
                if (dax != "") desc += " Expresión DAX: " + dax;
            }
            else
            {
                desc = string.Format("Columna '{0}' de la tabla '{1}'. Tipo de dato: {2}.", c.Name, t.Name, c.DataType);
                if (c is DataColumn && !string.IsNullOrWhiteSpace(((DataColumn)c).SourceColumn)
                    && ((DataColumn)c).SourceColumn != c.Name)
                {
                    desc += " Columna de origen: " + ((DataColumn)c).SourceColumn + ".";
                }
            }
            if (c.IsKey) desc += " Es clave de la tabla.";
            if (c.IsHidden) desc += " Oculta para el usuario final.";

            c.Description = desc;
            nColumnas++;
        }
    }

    // ---------- MEDIDAS ----------
    foreach (var m in t.Measures)
    {
        if (sobrescribir || string.IsNullOrWhiteSpace(m.Description))
        {
            string desc = string.Format("Medida '{0}' (tabla '{1}').", m.Name, t.Name);
            if (!string.IsNullOrWhiteSpace(m.DisplayFolder))
                desc += " Carpeta: " + m.DisplayFolder + ".";
            if (!string.IsNullOrWhiteSpace(m.FormatString))
                desc += " Formato: " + m.FormatString + ".";
            if (incluirDax)
            {
                string dax = Limpiar(m.Expression, maxLongitudDax);
                if (dax != "") desc += " Cálculo DAX: " + dax;
            }

            m.Description = desc;
            nMedidas++;
        }
    }
}

Info(string.Format("Descripciones añadidas -> Tablas: {0} | Columnas: {1} | Medidas: {2}",
    nTablas, nColumnas, nMedidas));
