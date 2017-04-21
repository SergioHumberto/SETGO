CREATE PROCEDURE [dbo].[ReporteRegistrados_PA]
(
	@IdCarrera INT
)
AS
BEGIN

	SELECT 
	P.Nombre
	, P.ApellidoPaterno
	, P.ApellidoMaterno
	, P.FechaNacimiento
	, P.Domicilio
	, P.Invitado
	, P.NumeroAccion
	, P.Telefono
	, P.Email
	, P.TelefonoEmergencia
	, C.Nombre AS [Categoria]
	, R.Nombre AS [Rama]
	, P.TransactionNumber
	, P.StatusPaypal
	FROM Participante P
	INNER JOIN ParticipanteXCarrera PxC ON PxC.IdParticipante = P.IdParticipante
	INNER JOIN Categoria C ON C.IdCategoria = PxC.IdCategoria
	INNER JOIN Rama R ON R.IdRama = PxC.IdRama
	WHERE PxC.IdCarrera = @IdCarrera

END
