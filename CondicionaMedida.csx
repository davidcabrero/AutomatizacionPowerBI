// Filtra solo las medidas seleccionadas
var measures = Selected.Measures;

// Recorre cada medida seleccionada
foreach (var m in measures)
{
    // Evita duplicar lógica si ya tiene IF
    if (m.Expression.Contains("IF(")) continue;

    var originalExpression = m.Expression;

// Envuelve la medida en lógica: BLANK si 0 o BLANK
    m.Expression =
$@"
VAR __base = {originalExpression}
RETURN
IF(
    ISBLANK(__base) || __base = 0,
    BLANK(),
    __base
)
";
}
