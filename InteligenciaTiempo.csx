// --- CONFIGURACIÓN ---
// Escribe aquí el nombre de tu tabla de fechas y columna de fecha
string columnaFecha = "'Date'[Date]"; 
// ---------------------

// Recorremos solo las medidas seleccionadas
foreach(var m in Selected.Measures) {
    
    // Definimos la carpeta donde se guardarán (dentro de la carpeta actual + "Time Intel")
    string carpeta = m.DisplayFolder; 
    if(string.IsNullOrEmpty(carpeta)) {
        carpeta = "Inteligencia de tiempo";
    } else {
        carpeta = carpeta + "\\Inteligencia de tiempo";
    }

    // 1. Crear Medida PY (Previous Year)
    string nombrePY = m.Name + " PY";
    string daxPY = "CALCULATE(" + m.DaxObjectName + ", SAMEPERIODLASTYEAR(" + columnaFecha + "))";
    
    var nuevaPY = m.Table.AddMeasure(nombrePY, daxPY, carpeta);
    nuevaPY.FormatString = m.FormatString; // Copiamos el formato (moneda, etc.)
    
    
    // 2. Crear Medida YoY (Variación Dinero: Actual - PY)
    string nombreYoY = m.Name + " YoY";
    // Referenciamos la medida PY recién creada usando corchetes
    string daxYoY = m.DaxObjectName + " - [" + nombrePY + "]";
    
    var nuevaYoY = m.Table.AddMeasure(nombreYoY, daxYoY, carpeta);
    nuevaYoY.FormatString = m.FormatString;


    // 3. Crear Medida YoY % (Variación Porcentual)
    string nombreYoYPorc = m.Name + " YoY %";
    string daxYoYPorc = "DIVIDE([" + nombreYoY + "], [" + nombrePY + "])";
    
    var nuevaYoYPorc = m.Table.AddMeasure(nombreYoYPorc, daxYoYPorc, carpeta);
    nuevaYoYPorc.FormatString = "#,0.0%;-#,0.0%;#,0.0%"; // Formato porcentaje


    // 4. Crear Medida YTD (Year To Date)
    string nombreYTD = m.Name + " YTD";
    string daxYTD = "TOTALYTD(" + m.DaxObjectName + ", " + columnaFecha + ")";
    
    var nuevaYTD = m.Table.AddMeasure(nombreYTD, daxYTD, carpeta);
    nuevaYTD.FormatString = m.FormatString;
}