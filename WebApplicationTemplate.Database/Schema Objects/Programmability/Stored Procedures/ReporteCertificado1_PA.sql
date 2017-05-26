CREATE PROCEDURE [dbo].[ReporteCertificado1_PA]
(
	@IdCarrera INT
	, @IdResultado INT = NULL
)
AS
BEGIN
	SELECT 
	R.Paterno + ' ' + R.Materno + ' ' + R.Nombres AS [NombreCompleto]
	, R.Lug_Gral AS [PosicionGeneral]
	, R.Sexo AS [Sexo]
	, R.T_Chip AS [TiempoChip]
	, R.Lug_Cat AS [PosicionPorCategoria]
	, R.Lug_Rama AS [PosicionSexo]
	FROM Resultados R
	INNER JOIN ConfiguracionResultados CR ON CR.IdConfiguracionResultados = R.IdConfiguracionResultados
	WHERE CR.IdCarrera = @IdCarrera
		AND (@IdResultado <= 0 OR R.IdResultado = @IdResultado)
END 