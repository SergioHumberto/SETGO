CREATE PROCEDURE [dbo].[ReporteCertificado1_PA]
(
	@IdCarrera INT
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
	WHERE R.IdCarrera = @IdCarrera
END 