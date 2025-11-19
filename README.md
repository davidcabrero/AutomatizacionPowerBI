# Automatización en Power BI

Este proyecto contiene scripts en C# para automatizar procesos comunes de Power BI con Tabular 2.

---

## Scripts Disponibles

### 1. `AutoDocModelo.csx`

Este script genera un documento con la documentación completa del modelo, incluyendo:

- Tablas presentes en el modelo.
- Medidas y cómo están formadas.
- Origen de los datos.
- Relaciones entre tablas.

Es útil para mantener actualizada la documentación técnica del modelo.

---

### 2. `DetectaNoUsoColumns.csx`

Este script revisa el modelo para detectar columnas que no están siendo utilizadas en el modelo, facilitando la optimización y limpieza del mismo.

---

### 3. `FormatoyCreacionJerarquias.csx`

Este script da el formato correcto a todas las medidas del modelo y crea las jerarquías de las fechas.

---

### 4. `CambioNombresMedidas.csx`

Este script encuentra todas las medidas en las que el nombre incluya la palabra que introduzca el usuario y sustituye la palabra por la que indique el usuario.

### 5. `CambioNombresMedidas.csx`

Este Script crea medidas de inteligencia de tiempo delas medidas seleccionadas por el usuario.
