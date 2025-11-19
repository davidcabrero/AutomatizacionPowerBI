// --- CONFIGURACIÓN ---
// 1. Escribe aquí la palabra que quieres QUITAR (entre comillas)
string buscar = "Acum"; 

// 2. Escribe aquí la palabra que quieres PONER (entre comillas)
string reemplazar = "Acumulado";
// ---------------------

int cambios = 0;

// Recorremos todas las medidas
foreach (var m in Model.AllMeasures) {
    
    // Si el nombre contiene la palabra...
    if (m.Name.Contains(buscar)) {
        
        // Reemplazamos
        m.Name = m.Name.Replace(buscar, reemplazar);
        
        // Contamos el cambio
        cambios++;
    }
}

// Mensaje final usando concatenación simple para evitar errores de versión
if (cambios > 0) {
    Info("Proceso terminado. Se han renombrado " + cambios + " medidas.");
} else {
    Warning("No se encontraron medidas con la palabra: " + buscar);
}