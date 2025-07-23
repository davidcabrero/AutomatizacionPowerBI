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
