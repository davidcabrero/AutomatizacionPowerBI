// Script para ocultar columnas técnicas de claves
foreach (var tabla in Model.Tables)
{
    foreach (var col in tabla.Columns)
    {
        if (col.Name.EndsWith("ID") || col.Name.Contains("Key") || col.Name.Contains("SK"))
        {
            col.IsHidden = true;
        }
    }
}
