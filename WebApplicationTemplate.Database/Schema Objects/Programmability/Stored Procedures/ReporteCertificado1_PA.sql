CREATE PROCEDURE [dbo].[ReporteCertificado1_PA]
(
	@IdCarrera INT
	, @IdResultado INT = NULL
)
AS
BEGIN
	SELECT 
	isnull(R.Paterno,'') + ' ' + isnull(R.Materno,'') + ' ' + isnull(R.Nombres,'') AS [NombreCompleto]
	, R.Lug_Gral AS [PosicionGeneral]
	, R.Sexo AS [Sexo]
	, R.T_Chip AS [TiempoChip]
	, R.Lug_Cat AS [PosicionCategoria]
	, R.Lug_Rama AS [PosicionSexo]
	, R.Edad AS [Edad]
	, R.T_Intermedio AS [TiempoIntermedio]
	, R.Ruta AS [Edicion]	
	, R.Equipo AS [Equipo]
	FROM Resultados R
	INNER JOIN ConfiguracionResultados CR ON CR.IdConfiguracionResultados = R.IdConfiguracionResultados
	WHERE CR.IdCarrera = @IdCarrera
		AND (@IdResultado <= 0 OR R.IdResultado = @IdResultado)
END 